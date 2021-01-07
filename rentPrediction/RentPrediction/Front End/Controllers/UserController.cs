using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentPrediction.BEModels.DTOs.User;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Infrastructure.Data;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;


namespace Front_End.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        public UserController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserDto[]>> GetAllUsers()
        {
            try
            {
                var users = _userManager.GetAllUsers();
                var userDtos = users.Select(user => _mapper.Map<UserDto>(user)).ToList();
                return Ok(userDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get users");
            }
        }
        [HttpGet]
        [Route("Email/{email}")]
        public async Task<ActionResult> Email(string email)
        {
            string subject = "Password reset";
            var user = await _userManager.GetByEmail(email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Nu este niciun utilizator asociat acestui email");
            }
            //new Password generator
            Random random1= new Random();
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 12; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            var newPassword = builder.ToString();
            string body = $"Buna, " + user.FirstName + ", noua ta parola este: " + newPassword + " . Poti sa o schimbi oricand din setari. O zi frumoasa!";
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("djane9083@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("djane9083@gmail.com", "paroladetest2");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        user.PasswordHash = PasswordHashing.Hash(newPassword);
                        await _userManager.UpdateUser(user);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userManager.GetUserById(id);
                var userModel = _mapper.Map<User,UserDto>(user);
                return Ok(userModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Nu este niciun astfel de utilizator");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser(UserDto userModel)
        {
            try
            {
                var newUserBrief = new UserBriefDto()
                {
                    Password = userModel.Password,
                    Username = userModel.Username
                };
                var existingUser = await _userManager.GetByEmail(userModel.Email);
                if (existingUser != null) return BadRequest(new { message = "Adresa de email este deja folosita" });
                 existingUser = await _userManager.GetByUsername(userModel.Username);
                if (existingUser != null) return BadRequest(new { message = "Numele de utilizator este deja folosit" });
                var user = _mapper.Map<UserDto, User>(userModel);
                await _userManager.AddUser(user);
                return await Authenticate(newUserBrief);
            }
            catch (Exception)
            {
                throw new Exception("Am intampinat o eroare");
            }
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<ActionResult<UserDto>> EditUser(UserDto userModel)
        {
            try
            {
                var user = _mapper.Map<UserDto, User>(userModel);
                var userEntity=await _userManager.UpdateUser(user);
                var userDto = _mapper.Map<User, UserDto>(userEntity);
                return userDto;
            }
            catch (Exception e)
            {
                throw new Exception("Could not save changes");
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("Reset")]
        public async Task<ActionResult<UserDto>> EditUserPassword(UserResetPasswordDto userModel)
        {
            var user = await _userManager.GetUserById(userModel.Id);
            if (PasswordHashing.Verify( userModel.OldPassword, user.PasswordHash))
            {
                user.PasswordHash = PasswordHashing.Hash(userModel.NewPassword);
                try
                {
                    var userEntity = await _userManager.UpdateUser(user);
                    var userDto = _mapper.Map<User, UserDto>(userEntity);
                    return userDto;
                }
                catch (Exception e)
                {
                    throw new Exception("Am intampinat o eroare");
                }
            }
            else
            {
                throw new Exception("Parola curenta nu coincide");
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDto>> Authenticate(UserBriefDto userModel)
        {
            try
            {
                var user = await _userManager.Authenticate(userModel);

                if (user == null)
                    return BadRequest(new { message = "Numele de utilizator sau parola nu au fost introduse corect" });


                var key = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
                var secretKey = new SymmetricSecurityKey(key);


                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.Name)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                var userDto = _mapper.Map<User, UserDto>(user);
                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    Id = user.Id,
                    Name = user.FirstName + " " + user.LastName,
                    RoleName = user.Role.Name,
                    Token = tokenString,
                    User= userDto
                });
            }
            catch (Exception)
            {
                throw new Exception("Am intampinat o eroare");
            }
        }
    }
}
