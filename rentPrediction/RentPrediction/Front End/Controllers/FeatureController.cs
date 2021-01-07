using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.Feature;
using RentPrediction.Business;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureController: ApiController
    {
        private readonly IFeatureManager _featureManager;
        private readonly IMapper _mapper;
        public FeatureController(IFeatureManager featureManager, IMapper mapper)
        {
            _featureManager = featureManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<FeatureDto[]>> GetAll()
        {
            try
            {
                var features = _featureManager.GetAllFeatures();
                var featureDtos = features.Select(feature => _mapper.Map<FeatureDto>(feature)).ToList();
                return Ok(featureDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get features");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeatureDto[]>> GetById(int id)
        {
            try
            {
                var feature = await _featureManager.GetFeatureById(id);
                var featureModel = _mapper.Map<FeatureDto>(feature);
                return Ok(featureModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no feature with id {id}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<FeatureDto[]>> Update(Feature featureModel)
        {
            try
            {
                var feature = await _featureManager.UpdateFeature(featureModel);
                var featureDto = _mapper.Map<FeatureDto>(feature);
                return Ok(featureDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<FeatureDto[]>> Add(Feature featureModel)
        {
            try
            {
                var feature = await _featureManager.AddFeature(featureModel);
                var featureDto = _mapper.Map<FeatureDto>(feature);
                return Ok(featureDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
