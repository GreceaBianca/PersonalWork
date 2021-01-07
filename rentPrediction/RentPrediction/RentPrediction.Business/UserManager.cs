using Microsoft.EntityFrameworkCore;
using RentPrediction.BEModels.DTOs.User;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Infrastructure.Data;
using RentPrediction.Repo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentPrediction.Business
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserManager( IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public IList<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers().Include(u=>u.Role).ToList();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> AddUser(User newUser)
        {
            newUser.PasswordHash = PasswordHashing.Hash(newUser.PasswordHash);
            if (newUser.Role != null) return await _userRepository.AddUser(newUser);
            var role= await _roleRepository.GetAllRoles().Where(r => r.Name == "Utilizatator de baza" )
                .FirstOrDefaultAsync();
            newUser.RoleId = role.Id;
            newUser.Role = role;
            return await _userRepository.AddUser(newUser);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task<User> Authenticate(UserBriefDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
                return null;

            var user = await _userRepository.GetAllUsers().Where(x => x.Username == userDto.Username).Include(u=>u.Role).FirstOrDefaultAsync();
           
            if (user == null)
                return null;
            if (PasswordHashing.Verify( userDto.Password, user.PasswordHash)) return user;
            return null;
        }


        public Task DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return user;
        }
        

        public async Task<User> GetByUsername(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            return user;
        }
    }
}
