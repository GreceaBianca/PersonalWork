using AutoMapper;
using RentPrediction.BEModels.DTOs.Property;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class PropertyMappingProfile:Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap<Property, PropertyDto>().ForMember(dest=>dest.Images, opt=>opt.MapFrom(src=>src.Galleries)).ReverseMap();
        }
    }
}
