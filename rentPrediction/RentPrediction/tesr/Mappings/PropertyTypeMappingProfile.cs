using AutoMapper;
using RentPrediction.BEModels.DTOs.PropertyType;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{

    public class PropertyTypeMappingProfile : Profile
    {
        public PropertyTypeMappingProfile()
        {
            CreateMap<PropertyType, PropertyTypeDto>().ReverseMap();
        }
    }
}
