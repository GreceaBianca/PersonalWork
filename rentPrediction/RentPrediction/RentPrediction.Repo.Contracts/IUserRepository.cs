using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        IQueryable<User> GetAllUsers();
        Task<User> GetByEmail(string email);
        Task<User> GetUserById(int id);
        Task<User> AddUser(User newUser);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
