using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.Address;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ApiController
    {
        private readonly IAddressManager _addressManager;
        private readonly IMapper _mapper;
        public AddressController(IAddressManager addressManager, IMapper mapper)
        {
            _addressManager = addressManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<AddressDto[]>> GetAll()
        {
            try
            {
                var addresss = _addressManager.GetAllAddresses();
                var addressDtos = addresss.Select(address => _mapper.Map<AddressDto>(address)).ToList();
                return Ok(addressDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get addresses");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto[]>> GetById(int id)
        {
            try
            {
                var address = await _addressManager.GetAddressById(id);
                var addressModel = _mapper.Map<AddressDto>(address);
                return Ok(addressModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no address with id {id}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<AddressDto[]>> Update(Address addressModel)
        {
            try
            {
                var address = await _addressManager.UpdateAddress(addressModel);
                var addressDto = _mapper.Map<AddressDto>(address);
                return Ok(addressDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto[]>> Add(Address addressModel)
        {
            try
            {
                var address = await _addressManager.AddAddress(addressModel);
                var addressDto = _mapper.Map<AddressDto>(address);
                return Ok(addressDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}
