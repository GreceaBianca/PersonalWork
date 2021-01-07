using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Business.Contracts
{
    public interface IPropertyTypeManager
    {
        IList<PropertyType> GetAllPropertyTypes();
        Task<PropertyType> GetPropertyTypeById(int id);
        Task<PropertyType> AddPropertyType(PropertyType newPropertyType);
        Task<PropertyType> UpdatePropertyType(PropertyType propertyType);
        Task DeletePropertyType(int id);
    }
}
