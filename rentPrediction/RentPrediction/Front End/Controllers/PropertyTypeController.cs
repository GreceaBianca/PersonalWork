using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.PropertyType;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTypeController : ApiController
    {
        private readonly IPropertyTypeManager _propertyTypeManager;
        private readonly IMapper _mapper;
        public PropertyTypeController(IPropertyTypeManager propertyTypeManager, IMapper mapper)
        {
            _propertyTypeManager = propertyTypeManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PropertyTypeDto[]>> GetAll()
        {
            try
            {
                var propertyTypes = _propertyTypeManager.GetAllPropertyTypes();
                var propertyTypeDtos = propertyTypes.Select(propertyType => _mapper.Map<PropertyTypeDto>(propertyType)).ToList();
                return Ok(propertyTypeDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get propertyTypes");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyTypeDto[]>> GetById(int id)
        {
            try
            {
                var propertyType = await _propertyTypeManager.GetPropertyTypeById(id);
                var propertyTypeModel = _mapper.Map<PropertyTypeDto>(propertyType);
                return Ok(propertyTypeModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no propertyType with id {id}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<PropertyTypeDto[]>> Update(PropertyType propertyTypeModel)
        {
            try
            {
                var propertyType = await _propertyTypeManager.UpdatePropertyType(propertyTypeModel);
                var propertyTypeDto = _mapper.Map<PropertyTypeDto>(propertyType);
                return Ok(propertyTypeDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<PropertyTypeDto[]>> Add(PropertyType propertyTypeModel)
        {
            try
            {
                var propertyType = await _propertyTypeManager.AddPropertyType(propertyTypeModel);
                var propertyTypeDto = _mapper.Map<PropertyTypeDto>(propertyType);
                return Ok(propertyTypeDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
