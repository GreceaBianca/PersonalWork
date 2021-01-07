using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.Business.Contracts;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IScraperManager _scraperManager;

        public ScraperController(IScraperManager scraperManager, IMapper mapper)
        {
            _scraperManager = scraperManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task CreateCrawler(int userId)
        {
            await _scraperManager.StartScrapping(userId);
        }

    }
}
