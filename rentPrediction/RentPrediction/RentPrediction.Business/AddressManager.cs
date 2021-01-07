using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;

namespace RentPrediction.Business
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository _addressRepository;
        public AddressManager(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public IList<Address> GetAllAddresses()
        {
            return _addressRepository.GetAllAddresses().ToList();
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _addressRepository.GetAddressById(id);
        }

        public async Task<Address> AddAddress(Address newAddress)
        {
            return await _addressRepository.AddAddress(newAddress);
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            return await _addressRepository.UpdateAddress(address);
        }

        public Task DeleteAddress(int id)
        {
            return _addressRepository.DeleteAddress(id);
        }
    }
}
