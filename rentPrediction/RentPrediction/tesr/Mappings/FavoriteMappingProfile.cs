using AutoMapper;
using RentPrediction.BEModels.DTOs.Favorite;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class FavoriteMappingProfile:Profile
    {
        public FavoriteMappingProfile()
        {
            CreateMap<Favorite, FavoriteDto>()
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PropertyId,
                    opt => opt.MapFrom(src => src.PropertyId))
                .ReverseMap();
        }
    }
}
