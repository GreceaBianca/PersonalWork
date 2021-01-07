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
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly DbContext _context;
        public PropertyTypeRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<PropertyType> GetAllPropertyTypes()
        {
            return _context.PropertyTypes.AsNoTracking();
        }

        public async Task<PropertyType> GetPropertyTypeById(int id)
        {
            return await _context.PropertyTypes.AsNoTracking().FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task<PropertyType> AddPropertyType(PropertyType newPropertyType)
        {
            var propertyType = await GetPropertyTypeById(newPropertyType.Id);
            if (propertyType != null)
            {
                throw new Exception("Property Type already exists!");
            }
            _context.PropertyTypes.Add(newPropertyType);
            await _context.SaveChangesAsync();
            return newPropertyType;
        }
        public async Task<PropertyType> UpdatePropertyType(PropertyType propertyType)
        {
            var oldPropertyType = await GetPropertyTypeById(propertyType.Id);
            if (oldPropertyType == null || oldPropertyType.IsArchived)
            {
                throw new Exception("Property Type can not be found");
            }
            _context.PropertyTypes.Update(propertyType);
            await _context.SaveChangesAsync();
            return propertyType;
        }

        public async Task DeletePropertyType(int id)
        {
            var propertyType = await GetPropertyTypeById(id);
            if (propertyType == null || propertyType.IsArchived)
            {
                throw new Exception("Property Type can not be found");
            }
            propertyType.IsArchived = true;
            propertyType.ArchivedDate = DateTime.UtcNow;
            await UpdatePropertyType(propertyType);
            return;
        }
    }
}
