using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IRoleRepository
    {
        IQueryable<Role> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> AddRole(Role newRole);
        Task<Role> UpdateRole(Role role);
        Task DeleteRole(int id);
    }
}
