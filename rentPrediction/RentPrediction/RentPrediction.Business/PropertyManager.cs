using Microsoft.EntityFrameworkCore;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentPrediction.Business
{
    public class PropertyManager : IPropertyManager
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyManager( IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public IList<Property> GetAllProperties()
        {
            return _propertyRepository.GetAllProperties()
                .AsNoTracking()
                .Where(p => !p.IsArchived)
                .Include(p => p.Feature)
                .ThenInclude(f => f.Partitioning)
                .Include(p => p.Address)
                .Include(p => p.PropertyType)
                .Include(p => p.Galleries)
                .ToList();
        }
        public async Task<IList<Property>> GetAll()
        {
            return await _propertyRepository.GetAllProperties()
                .Include(p => p.Feature)
                .ThenInclude(f => f.Partitioning)
                .Include(p => p.Address)
                .Include(p => p.PropertyType)
                .Include(p => p.Galleries)
                .ToListAsync();
        }

        public IList<Property> GetAllUserProperties(int userId)
        {
            return _propertyRepository.GetAllProperties()
                .AsNoTracking()
                .Where(p=>p.UserId==userId)
                .Include(p => p.Feature)
                .ThenInclude(f => f.Partitioning)
                .Include(p => p.Address)
                .Include(p => p.PropertyType)
                .Include(p => p.Galleries)
                .ToList();
        }


        public async Task<Property> GetPropertyById(int id)
        {
            return await _propertyRepository.GetPropertyById(id);
        }

        public async Task<Property> GetPropertyByIdNoInclude(int id)
        {
            return await _propertyRepository.GetPropertyByIdNoInclude(id);
        }
        public async Task<Property> GetPropertyByURL(string url)
        {
            return await _propertyRepository.GetPropertyByURL(url);
        }

        public async Task<Property> AddProperty(Property newProperty)
        {
            return await _propertyRepository.AddProperty(newProperty);
        }

        public async Task<Property> UpdateProperty(Property property)
        {
            return await _propertyRepository.UpdateProperty(property);
        }

        public Task DeleteProperty(int id)
        {
            return _propertyRepository.DeleteProperty(id);
        }
    }
}
