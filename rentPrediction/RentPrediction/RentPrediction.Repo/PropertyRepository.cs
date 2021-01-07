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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DbContext _context;
        public PropertyRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<Property> GetAllProperties()
        {
            return _context.Properties;
        }

        public async Task<Property> GetPropertyById(int id)
        {
            return await _context.Properties.AsNoTracking()
                .Include(p => p.Feature)
                .ThenInclude(f => f.Partitioning)
                .Include(p => p.Address)
                .Include(p => p.Galleries)
                .Include(p => p.PropertyType)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsArchived);
        }
        public async Task<Property> GetPropertyByIdNoInclude(int id)
        {
            return await _context.Properties.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsArchived);
        }

        public async Task<Property> GetPropertyByURL(string url)
        {
            return await _context.Properties
                .AsNoTracking()
                .Include(p => p.Feature)
                .ThenInclude(f => f.Partitioning)
                .Include(p => p.Address)
                .Include(p => p.PropertyType)
                .FirstOrDefaultAsync(p => p.URL == url && !p.IsArchived);
        }

        public async Task<Property> AddProperty(Property newProperty)
        {
            var property = await GetPropertyById(newProperty.Id);
            if (property != null)
            {
                throw new Exception("Property already exists!");
            }
            _context.Properties.Add(newProperty);
            await _context.SaveChangesAsync();
            return newProperty;
        }
        public async Task<Property> UpdateProperty(Property property)
        {
            //var oldProperty = await GetPropertyById(property.Id);
            //if (oldProperty == null || oldProperty.IsArchived)
            //{
            //    throw new Exception("Property can not be found");
            //}
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task DeleteProperty(int id)
        {
            var property = await GetPropertyById(id);
            if (property == null || property.IsArchived)
            {
                throw new Exception("Property can not be found");
            }
            property.IsArchived = true;
            property.ArchivedDate = DateTime.UtcNow;
            await UpdateProperty(property);
            return;
        }
    }
}
