using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Front_End.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using RentPrediction.Business.Contracts;
using RentPrediction.Business.ML;

namespace Front_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MLController : ApiController
    {
        private readonly IMLManager _MLManager;
        public MLController(IMLManager MLManager)
        {
            _MLManager = MLManager;
        }

        [HttpGet]
        public Task Test()
        {
          //  _MLManager.MainProgram();
            return null;
        }

        [HttpGet]
        [Route("engine")]
        public Task TestEngine()
        {
           // _MLManager.engine();
            return null;
        }
    }
}
