using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.Data.Entities;

namespace RentPrediction.Business.Contracts
{
    public interface IFeatureManager
    {
        IList<FeatureDto> GetAllFeatures();
        Task<Feature> GetFeatureById(int id);
        Task<Feature> AddFeature(Feature newFeature);
        Task<Feature> UpdateFeature(Feature feature);
        Task DeleteFeature(int id);
    }
}
