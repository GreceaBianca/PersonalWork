using AutoMapper;
using RentPrediction.BEModels.DTOs.User;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserRole,
                    opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();
            CreateMap<User, UserBriefDto>()
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();
        }
    }
}
