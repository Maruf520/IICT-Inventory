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
    }
}
