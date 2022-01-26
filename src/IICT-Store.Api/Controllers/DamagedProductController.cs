using IICT_Store.Services.DamagedProductServices;
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
    public class DamagedProductController : ControllerBase
    {
        private readonly IDamagedProductService damagedProductService;
        public DamagedProductController(IDamagedProductService damagedProductService)
        {
            this.damagedProductService = damagedProductService;
        }

        [HttpPost]
        public async Task<IActionResult> DamagedProduct(long id)
        {
            var damaged = await damagedProductService.DamageProduct( id);
            return Ok(damaged);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var damage = await damagedProductService.GetAllDamagedProduct();
            return Ok(damage);
        }


        //Get all damaged product by ProductId.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var damage = await damagedProductService.GetDamagedProductById(id);
            return Ok(damage);
        }
    }
}
