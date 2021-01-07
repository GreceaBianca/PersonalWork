using System.Collections.Generic;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Business.Contracts
{
    public interface IPropertyManager
    {
        Task<IList<Property>> GetAll();
        IList<Property> GetAllProperties();
        IList<Property> GetAllUserProperties(int userId);
        Task<Property> GetPropertyById(int id);
        Task<Property> GetPropertyByIdNoInclude(int id);
        Task<Property> GetPropertyByURL(string url);
        Task<Property> AddProperty(Property newProperty);
        Task<Property> UpdateProperty(Property property);
        Task DeleteProperty(int id);
    }
}
