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
    public class AddressRepository : IAddressRepository
    {
        private readonly DbContext _context;
        public AddressRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<Address> GetAllAddresses()
        {
            return _context.Addresses;
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Address> AddAddress(Address newAddress)
        {
            var address = await GetAddressById(newAddress.Id);
            if (address != null)
            {
                throw new Exception("Address already exists!");
            }
            _context.Addresses.Add(newAddress);
            await _context.SaveChangesAsync();
            return newAddress;
        }
        public async Task<Address> UpdateAddress(Address address)
        {
            var oldAddress = await GetAddressById(address.Id);
            if (oldAddress == null || oldAddress.IsArchived)
            {
                throw new Exception("Address can not be found");
            }
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task DeleteAddress(int id)
        {
            var address = await GetAddressById(id);
            if (address == null || address.IsArchived)
            {
                throw new Exception("Address can not be found");
            }
            address.IsArchived = true;
            address.ArchivedDate = DateTime.UtcNow;
            await UpdateAddress(address);
            return;
        }
    }
}
