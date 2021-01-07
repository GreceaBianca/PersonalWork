using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.BEModels.DTOs.User;
using RentPrediction.Data.Entities;

namespace RentPrediction.Business.Contracts
{
    public interface IUserManager
    {
        Task<User> GetByUsername(string username);
        IList<User> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User newUser);
        Task<User> UpdateUser(User role);
        Task<User> Authenticate(UserBriefDto user);
        Task DeleteUser(int id);
        Task<User> GetByEmail(string email);
    }
}
