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
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _context;
        public RoleRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<Role> GetAllRoles()
        {
            return _context.Roles;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> AddRole(Role newRole)
        {
            var role = await GetRoleById(newRole.Id);
            if (role != null)
            {
                throw new Exception("Role already exists!");
            }
            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }
        public async Task<Role> UpdateRole(Role role)
        {
            var oldRole = await GetRoleById(role.Id);
            if (oldRole == null || oldRole.IsArchived)
            {
                throw new Exception("Role can not be found");
            }
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task DeleteRole(int id)
        {
            var role = await GetRoleById(id);
            if (role == null || role.IsArchived)
            {
                throw new Exception("Role can not be found");
            }
            role.IsArchived = true;
            role.ArchivedDate = DateTime.UtcNow;
            await UpdateRole(role);
            return;
        }
    }
}
