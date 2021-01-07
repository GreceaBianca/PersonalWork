using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.Data.Entities;

namespace RentPrediction.Repo.Contracts
{
    public interface IFeatureRepository
    {
        IQueryable<FeatureDto> GetAllFeatures();
        Task<Feature> GetFeatureById(int id);
        Task<Feature> AddFeature(Feature newFeature);
        Task<Feature> UpdateFeature(Feature feature);
        Task DeleteFeature(int id);
    }
}
