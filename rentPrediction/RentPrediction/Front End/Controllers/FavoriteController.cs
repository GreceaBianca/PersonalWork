using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.Favorite;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ApiController
    {
        private readonly IFavoriteManager _favoriteManager;
        private readonly IMapper _mapper;
        public FavoriteController(IFavoriteManager favoriteManager, IMapper mapper)
        {
            _favoriteManager = favoriteManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<FavoriteDto[]>> GetAll()
        {
            try
            {
                var favorites = _favoriteManager.GetAllFavorites();
                var favoriteDtos = favorites.Select(favorite => _mapper.Map<FavoriteDto>(favorite)).ToList();
                return Ok(favoriteDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get favorites");
            }
        }
        [HttpGet]
        [Route("User/{userId}")]
        public async Task<ActionResult<FavoriteDto[]>> GetAllByUserId(int userId)
        {
            try
            {
                var favorites = _favoriteManager.GetAllFavoritesByUserId(userId);
                var favoriteDtos = favorites.Select(favorite =>
                {
                    if (favorite.Property.Galleries.Count > 0)
                    {
                        foreach (var propertyGallery in favorite.Property.Galleries)
                        {
                            propertyGallery.ImageURL = propertyGallery.ImageURL.Replace(Path.DirectorySeparatorChar, '/');
                        }
                    }


                    return _mapper.Map<FavoriteDto>(favorite);
                }).ToList();
                return Ok(favoriteDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get favorites");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteDto>> GetById(int id)
        {
            try
            {
                var favorite = await _favoriteManager.GetFavoriteById(id);
                var favoriteModel = _mapper.Map<FavoriteDto>(favorite);
                return Ok(favoriteModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no favorite with id {id}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var favorite =  _favoriteManager.DeleteFavorite(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no favorite with id {id}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<FavoriteDto[]>> Update(Favorite favoriteModel)
        {
            try
            {
                var favorite = await _favoriteManager.UpdateFavorite(favoriteModel);
                var favoriteDto = _mapper.Map<FavoriteDto>(favorite);
                return Ok(favoriteDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<FavoriteDto[]>> Add(Favorite favoriteModel)
        {
            try
            {
                var favorite = await _favoriteManager.AddFavorite(favoriteModel);
                var favoriteDto = _mapper.Map<FavoriteDto>(favorite);
                return Ok(favoriteDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
