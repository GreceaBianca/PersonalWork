using AutoMapper;
using RentPrediction.BEModels.DTOs.Gallery;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class GalleryMappingProfile:Profile
    {
        public GalleryMappingProfile()
        {
            CreateMap<Gallery, GalleryDto>().ReverseMap();
        }
    }
}
