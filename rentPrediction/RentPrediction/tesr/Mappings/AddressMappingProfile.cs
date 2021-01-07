using AutoMapper;
using RentPrediction.BEModels.DTOs.Address;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class AddressMappingProfile:Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }

}
