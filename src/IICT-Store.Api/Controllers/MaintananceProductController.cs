﻿using Microsoft.AspNetCore.Http;
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
    public class MaintananceProductController : BaseController
    {
        private readonly IMaintananceProductService maintananceProductService;

        public MaintananceProductController(IMaintananceProductService maintananceProductService)
        {
            this.maintananceProductService = maintananceProductService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintananceProductDto createMaintananceProduct)
        {
            var maintanaceProduct = await maintananceProductService.Create(createMaintananceProduct, GetuserId());
            return Ok(maintanaceProduct);
        }
        [HttpPost("repair")]
        public async Task<IActionResult> RepairOrDamage(CreateMaintananceProductDto createMaintananceProduct)
        {
            var maintanaceProduct = await maintananceProductService.RepairOrDamage(createMaintananceProduct);
            return Ok(maintanaceProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetByProductIdAndSerial(long productId, long serialId)
        {
            var product = await maintananceProductService.GetByProductSerial(productId, serialId);
            return Ok(product);
        }
    }
}
