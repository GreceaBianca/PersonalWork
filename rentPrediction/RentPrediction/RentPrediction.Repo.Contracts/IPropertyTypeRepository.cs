using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IPropertyTypeRepository
    {
        IQueryable<PropertyType> GetAllPropertyTypes();
        Task<PropertyType> GetPropertyTypeById(int id);
        Task<PropertyType> AddPropertyType(PropertyType newPropertyType);
        Task<PropertyType> UpdatePropertyType(PropertyType propertyType);
        Task DeletePropertyType(int id);
    }
}
