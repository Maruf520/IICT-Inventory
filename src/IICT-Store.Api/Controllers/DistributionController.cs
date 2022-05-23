using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Services.DistributionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace IICT_Store.Api.Controllers
{
    [Route("api/distributions")]
    [ApiController]
    public class DistributionController : BaseController
    {
        private readonly ILogger<DistributionController> logger;
        private readonly IDistributionService distributionService;
        public DistributionController(IDistributionService distributionService, ILogger<DistributionController> logger)
        {
            this.distributionService = distributionService;
            this.logger = logger;
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateDistribution([FromBody] CreateDistributionDto createDistributionDto)
        {
            this.logger.LogInformation($"CreateDistribution STARTED with requestBody: {JsonConvert.SerializeObject(createDistributionDto)}");
            var distribution = await distributionService.CreateNew(createDistributionDto, GetuserId());
            if (distribution.StatusCode == HttpStatusCode.OK)
            {
                this.logger.LogInformation($"CreateDistribution ENDED with OK response");
                return Ok(distribution);
            }
            else
            {
                this.logger.LogInformation($"CreateDistribution ENDED with BadRequest response");
                return BadRequest(distribution);
            }
        }
        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var distribution = await distributionService.GetById(id);
            return Ok(distribution);
        }
        [Authorize(Roles = "User")]
        [HttpGet("roomno/{id}")]
        public async Task<IActionResult> GetByRoomNo(int id)
        {
            var distribution = await distributionService.GetByRoomNo(id);
            return Ok(distribution);
        }
        [Authorize(Roles = "User")]
        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetByPersonId(long id)
        {
            var distribution = await distributionService.GetByPersonId(id);
            return Ok(distribution);
        }
        [Authorize(Roles = "User")]
        //get all distributions by product Id
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetALlDistributionByProductId(long id)
        {
            var distribution = await distributionService.GetAllDistributionByProductId(id);
            return Ok(distribution);
        }
        [Authorize(Roles = "User")]
        [HttpGet("product/serial/{id}")]
        public async Task<IActionResult> GetDistributionByProductNoId(long id)
        {
            var distribution = await distributionService.GetDistributionByProductNoId(id);
            return Ok(distribution);
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetDistribution([FromQuery] long productId, [FromQuery] long personId, [FromQuery] int roomNo)
        {
            var distritution = await distributionService.GetDirstribution(productId, personId, roomNo);
            return Ok(distritution);
        }
    }
}
