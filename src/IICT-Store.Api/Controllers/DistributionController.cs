using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Services.DistributionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionController : ControllerBase
    {
        private readonly IDistributionService distributionService;
        public DistributionController(IDistributionService distributionService)
        {
            this.distributionService = distributionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistribution(CreateDistributionDto createDistributionDto)
        {
            var distribution = await distributionService.Create(createDistributionDto);
            return Ok(distribution);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var distribution = await distributionService.GetById(id);
            return Ok(distribution);
        }

        [HttpGet("roomno/{id}")]
        public async Task<IActionResult> GetByRoomNo(int id)
        {
            var distribution = await distributionService.GetByRoomNo(id);
            return Ok(distribution);
        }
        
        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetByPersonId(long id)
        {
            var distribution = await distributionService.GetByPersonId(id);
            return Ok(distribution);
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetALlDistributionByProductId(long id)
        {
            var distribution = await distributionService.GetAllDistributionByProductId(id);
            return Ok(distribution);
        }
    }
}
