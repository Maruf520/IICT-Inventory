using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Services.ReturnProductServices;
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
    public class ReturnController : BaseController
    {
        private readonly IReturnProductService returnProductService;
        public ReturnController(IReturnProductService returnProductService)
        {
            this.returnProductService = returnProductService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReturn(CreateReturnProductDto createReturnDto, int id)
        {
            var returnProduct  = await returnProductService.CreateReturnProduct(createReturnDto, id, GetuserId());
            return Ok(returnProduct);
        }
    }
}
