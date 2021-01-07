using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SidekickApp.Controllers
{
    [ApiController]
    [Route("api/Sidekick")]
    public class SidekickController : ControllerBase
    {

        private readonly ISidekickManager _manager;

        public SidekickController(ISidekickManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IList<Sidekick>> GetAll()
        {
            return await _manager.GetSidekicks();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Sidekick> Get(int id)
        {
            return await _manager.GetSidekickById(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async void Delete(int id)
        {
            _manager.Delete(id);
        }

        [HttpPut]
        public async Task<Sidekick> Update(Sidekick sidekick)
        {
            return await _manager.Update(sidekick);
        }

        [HttpPost]
        public async Task<Sidekick> Add(Sidekick sidekick)
        {
            return await _manager.Add(sidekick);
        }
    }

}
