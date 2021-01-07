using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IPropertyRepository
    {
        IQueryable<Property> GetAllProperties();
        Task<Property> GetPropertyById(int id);
        Task<Property> GetPropertyByIdNoInclude(int id);
        Task<Property> GetPropertyByURL(string url);
        Task<Property> AddProperty(Property newProperty);
        Task<Property> UpdateProperty(Property address);
        Task DeleteProperty(int id);
    }
}
