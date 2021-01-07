using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PlanetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetController : ControllerBase
    {

        private readonly IPlanetManager _manager;

        public PlanetController(IPlanetManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<List<Planet>> GetAll()
        {
            return _manager.GetPlanets().Include(p=>p.Hero).ThenInclude(h=>h.Sidekick).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Planet> Get(int id)
        {
            return await _manager.GetPlanetById(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async void Delete(int id)
        {
            _manager.Delete(id);
        }

        [HttpPut]
        public async Task<Planet> Update(Planet planet)
        {
            return await _manager.Update(planet);
        }

        [HttpPost]
        public async Task<Planet> Add(Planet planet)
        {
            return await _manager.Add(planet);
        }
    }

}
