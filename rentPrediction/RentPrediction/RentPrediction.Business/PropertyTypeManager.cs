using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentPrediction.Business
{
    public class PropertyTypeManager : IPropertyTypeManager
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        public PropertyTypeManager( IPropertyTypeRepository propertyTypeRepository)
        {
            _propertyTypeRepository = propertyTypeRepository;
        }
        public IList<PropertyType> GetAllPropertyTypes()
        {
            return _propertyTypeRepository.GetAllPropertyTypes().ToList();
        }

        public async Task<PropertyType> GetPropertyTypeById(int id)
        {
            return await _propertyTypeRepository.GetPropertyTypeById(id);
        }

        public async Task<PropertyType> AddPropertyType(PropertyType newPropertyType)
        {
            return await _propertyTypeRepository.AddPropertyType(newPropertyType);
        }

        public async Task<PropertyType> UpdatePropertyType(PropertyType propertyType)
        {
            return await _propertyTypeRepository.UpdatePropertyType(propertyType);
        }

        public Task DeletePropertyType(int id)
        {
            return _propertyTypeRepository.DeletePropertyType(id);
        }
    }
}
