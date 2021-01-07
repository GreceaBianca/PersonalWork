using AutoMapper;
using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.Data.Entities;

namespace RentPrediction.BEModels.Mappings
{
    public class FeatureMappingProfile:Profile
    {
        public FeatureMappingProfile()
        {
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
