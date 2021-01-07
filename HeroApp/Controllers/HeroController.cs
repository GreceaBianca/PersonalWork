using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HeroApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        
        private readonly IHeroManager _manager;

        public HeroController(IHeroManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<List<Hero>> GetAll()
        {
            return _manager.GetHeroes().Include(h=>h.Sidekick).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Hero> Get(int id)
        {
            return await _manager.GetHeroById(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async void Delete(int id)
        {
             _manager.Delete(id);
        }

        [HttpPut]
        public async Task<Hero> Update(Hero hero)
        {
            return await _manager.Update(hero);
        }

        [HttpPost]
        public async Task<Hero> Add(Hero hero)
        {
            return await _manager.Add(hero);
        }
    }
}
