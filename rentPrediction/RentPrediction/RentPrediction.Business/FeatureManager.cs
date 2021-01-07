using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentPrediction.Business
{
    public class FeatureManager : IFeatureManager
    {
        private readonly IFeatureRepository _featureRepository;
        public FeatureManager( IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }
        public IList<FeatureDto> GetAllFeatures()
        {
            return _featureRepository.GetAllFeatures().ToList();
        }

        public async Task<Feature> GetFeatureById(int id)
        {
            return await _featureRepository.GetFeatureById(id);
        }

        public async Task<Feature> AddFeature(Feature newFeature)
        {
            return await _featureRepository.AddFeature(newFeature);
        }

        public async Task<Feature> UpdateFeature(Feature feature)
        {
            return await _featureRepository.UpdateFeature(feature);
        }

        public Task DeleteFeature(int id)
        {
            return _featureRepository.DeleteFeature(id);
        }
    }
}
