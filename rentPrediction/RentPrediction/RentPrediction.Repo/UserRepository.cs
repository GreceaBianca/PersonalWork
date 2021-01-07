using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using DbContext = RentPrediction.Data.DbContext;

namespace RentPrediction.Repo
{
    public class UserRepository:IUserRepository
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Username.Equals(username));
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.Include(u=>u.Role).AsNoTracking().FirstOrDefaultAsync(u=>u.Id==id);
        }

        public async Task<User> AddUser(User newUser)
        {
            var user = await GetUserById(newUser.Id);
            if (user != null)
            {
                throw new Exception("User already exists!");
            }
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
        public async Task<User> UpdateUser(User user)
        {
            var oldUser = await GetUserById(user.Id);
            if (oldUser == null || oldUser.IsArchived)
            {
                throw new Exception("User can not be found");
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user==null||user.IsArchived)
            {
                throw new Exception("User can not be found");
            }
            user.IsArchived = true;
            user.ArchivedDate=DateTime.UtcNow;
            await UpdateUser(user);
            return;
        }
    }
}
