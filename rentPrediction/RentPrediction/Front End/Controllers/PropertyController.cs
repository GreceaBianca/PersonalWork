using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.Property;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IPropertyManager _propertyManager;

        public PropertyController(IPropertyManager propertyManager, IMapper mapper)
        {
            _propertyManager = propertyManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PropertyDto[]>> GetAll()
        {
            try
            {
                var properties = _propertyManager.GetAllProperties();
                var propertyDtos = properties.Select(property =>
                {
                    foreach (var propertyGallery in property.Galleries)
                    {
                        propertyGallery.ImageURL = propertyGallery.ImageURL.Replace(Path.DirectorySeparatorChar, '/');
                    }

                    return _mapper.Map<PropertyDto>(property);
                }).ToList();
                return Ok(propertyDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get properties");
            }
        }
        [HttpGet]
        [Route("User/{userId}")]
        public async Task<ActionResult<PropertyDto[]>> GetUserProperties(int userId)
        {
            try
            {
                var properties = _propertyManager.GetAllUserProperties(userId);
                var propertyDtos = properties.Select(property =>
                {
                    foreach (var propertyGallery in property.Galleries)
                    {
                        propertyGallery.ImageURL = propertyGallery.ImageURL.Replace(Path.DirectorySeparatorChar, '/');
                    }
                    return _mapper.Map<PropertyDto>(property);
                }).ToList();
                return Ok(propertyDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get properties");
            }
        }
  
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetById(int id)
        {
            try
            {
                var property = await _propertyManager.GetPropertyById(id);
                foreach (var propertyGallery in property.Galleries)
                {
                    propertyGallery.ImageURL = propertyGallery.ImageURL.Replace(Path.DirectorySeparatorChar, '/');
                }
                var propertyModel = _mapper.Map<PropertyDto>(property);
                return Ok(propertyModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no property with id {id}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<PropertyDto[]>> Update(Property propertyModel)
        {
            try
            {
                var property = await _propertyManager.UpdateProperty(propertyModel);
                var propertyDto = _mapper.Map<PropertyDto>(property);
                return Ok(propertyDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int  id)
        {
            try
            {
                var property =  _propertyManager.DeleteProperty(id);
                return Ok();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<PropertyDto[]>> Add(Property propertyModel)
        {
            try
            {
                var property = await _propertyManager.AddProperty(propertyModel);
                var propertyDto = _mapper.Map<PropertyDto>(property);
                return Ok(propertyDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}