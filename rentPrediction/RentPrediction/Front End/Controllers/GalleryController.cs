using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.Gallery;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ApiController
    {
        private readonly IGalleryManager _galleryManager;
        private readonly IMapper _mapper;
        public GalleryController(IGalleryManager galleryManager, IMapper mapper)
        {
            _galleryManager = galleryManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GalleryDto[]>> GetAll()
        {
            try
            {
                var gallerys = _galleryManager.GetAllGalleries();
                var galleryDtos = gallerys.Select(gallery => _mapper.Map<GalleryDto>(gallery)).ToList();
                return Ok(galleryDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get gallerys");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GalleryDto[]>> GetById(int id)
        {
            try
            {
                var gallery = await _galleryManager.GetGalleryById(id);
                var galleryModel = _mapper.Map<GalleryDto>(gallery);
                return Ok(galleryModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no gallery with id {id}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<GalleryDto[]>> Update(Gallery galleryModel)
        {
            try
            {
                var gallery = await _galleryManager.UpdateGallery(galleryModel);
                var galleryDto = _mapper.Map<GalleryDto>(gallery);
                return Ok(galleryDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<GalleryDto[]>> Add(Gallery galleryModel)
        {
            try
            {
                var gallery = await _galleryManager.AddGallery(galleryModel);
                var galleryDto = _mapper.Map<GalleryDto>(gallery);
                return Ok(galleryDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
