using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentPrediction.Business
{
    public class RoleManager:IRoleManager
    {
        private readonly IRoleRepository _roleRepository;
        public RoleManager( IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public IList<Role> GetAllRoles()
        {
            return  _roleRepository.GetAllRoles().ToList();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleRepository.GetRoleById(id);
        }

        public async Task<Role> AddRole(Role newRole)
        {
            return await _roleRepository.AddRole(newRole);
        }

        public async Task<Role> UpdateRole(Role role)
        {
            return await  _roleRepository.UpdateRole(role);
        }

        public Task DeleteRole(int id)
        {
            return  _roleRepository.DeleteRole(id);
        }
    }
}
