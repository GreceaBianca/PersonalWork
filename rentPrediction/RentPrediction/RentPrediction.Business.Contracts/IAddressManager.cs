using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.Data.Entities;

namespace RentPrediction.Business.Contracts
{
    public interface IAddressManager
    {
        IList<Address> GetAllAddresses();
        Task<Address> GetAddressById(int id);
        Task<Address> AddAddress(Address newAddress);
        Task<Address> UpdateAddress(Address address);
        Task DeleteAddress(int id);
    }
}
