using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Services.MaintananceProductService;

namespace IICT_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintananceProductController : ControllerBase
    {
        private readonly IMaintananceProductService maintananceProductService;

        public MaintananceProductController(IMaintananceProductService maintananceProductService)
        {
            this.maintananceProductService = maintananceProductService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintananceProductDto createMaintananceProduct)
        {
            var maintanaceProduct = await maintananceProductService.Create(createMaintananceProduct);
            return Ok(maintanaceProduct);
        }
    }
}
