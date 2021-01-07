using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.Data.Entities;
using RentPrediction.Repo.Contracts;
using DbContext = RentPrediction.Data.DbContext;

namespace RentPrediction.Repo
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly DbContext _context;
        public FeatureRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<FeatureDto> GetAllFeatures()
        {
            return _context.Features.Select(it => new FeatureDto
            {
                HasBalcony = it.HasBalcony
            });
        }

        public async Task<Feature> GetFeatureById(int id)
        {
            return await _context.Features.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Feature> AddFeature(Feature newFeature)
        {
            var feature = await GetFeatureById(newFeature.Id);
            if (feature != null)
            {
                throw new Exception("Feature already exists!");
            }
            _context.Features.Add(newFeature);
            await _context.SaveChangesAsync();
            return newFeature;
        }
        public async Task<Feature> UpdateFeature(Feature feature)
        {
            var oldFeature = await GetFeatureById(feature.Id);
            if (oldFeature == null || oldFeature.IsArchived)
            {
                throw new Exception("Feature can not be found");
            }
            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
            return feature;
        }

        public async Task DeleteFeature(int id)
        {
            var feature = await GetFeatureById(id);
            if (feature == null || feature.IsArchived)
            {
                throw new Exception("Feature can not be found");
            }
            feature.IsArchived = true;
            feature.ArchivedDate = DateTime.UtcNow;
            await UpdateFeature(feature);
            return;
        }
    }
}
