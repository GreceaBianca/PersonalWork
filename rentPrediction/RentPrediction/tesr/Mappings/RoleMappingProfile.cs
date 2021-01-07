using AutoMapper;
using RentPrediction.BEModels.DTOs.Role;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class RoleMappingProfile:Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
