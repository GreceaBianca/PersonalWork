using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.BEModels.DTOs.Role;
using RentPrediction.Business.Contracts;
using RentPrediction.Data.Entities;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ApiController
    {
        private readonly IRoleManager _roleManager;
        private readonly IMapper _mapper;
        public RoleController(IRoleManager roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<RoleDto[]>> GetAll()
        {
            try
            {
                var roles = _roleManager.GetAllRoles();
                var roleDtos = roles.Select(role => _mapper.Map<RoleDto>(role)).ToList();
                return Ok(roleDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not get roles");
            }
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto[]>> GetById(int id)
        {
            try
            {
                var role = await _roleManager.GetRoleById(id);
                var roleModel = _mapper.Map<RoleDto>(role);
                return Ok(roleModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"There is no role with id {id}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<RoleDto[]>> Update(Role roleModel)
        {
            try
            {
                var role = await _roleManager.UpdateRole(roleModel);
                var roleDto = _mapper.Map<RoleDto>(role);
                return Ok(roleDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto[]>> Add(Role roleModel)
        {
            try
            {
                var role = await _roleManager.AddRole(roleModel);
                var roleDto = _mapper.Map<RoleDto>(role);
                return Ok(roleDto);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
