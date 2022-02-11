using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Services.ProductNumberServices;
using IICT_Store.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IProductNumberService productNumberService;
        public ProductController(IProductService productService, IProductNumberService productNumberService)
        {
            this.productService = productService;
            this.productNumberService = productNumberService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
        {
            var product = await productService.CreateProduct(createProductDto);
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAction(long id)
        {
            var product = await productService.GetProductById(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await productService.GetALlProduct();
            return Ok(products);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetProdctByCategoryId(long id)
        {
            var product = await productService.GetProductByCategoryId(id);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(CreateProductDto createProductDto, long id)
        {
            var product = await productService.UpdateProduct(createProductDto, id);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeteteProduct(long id)
        {
            var product = await productService.DeleteProduct(id);
            return Ok(product);
        }

        [HttpPost("{id}/serial")]
        public async Task<IActionResult> InsertProductNo(long id, CreateProductNoDto createProductNoDto)
        {
            var product = await productNumberService.InsertProductNo(id, createProductNoDto);
            return Ok(product);
        }

        [HttpGet("serial/{id}")]
        public async Task<IActionResult> GetProductBySerialNo(long id)
        {
            var serial = await productService.GetProductBySerialNo(id);
            return Ok(serial);
        }

        [HttpGet("available/{id}")]
        public async Task<IActionResult> GetAvailableProduct(long id)
        {
            var product = await productService.GetAllAvailableProductno(id);
            return Ok(product);
        }
        [HttpGet("return/{id}")]
        public async Task<IActionResult> ReturnTOStock(long id)
        {
            var product = await productService.ReturnProductToStore(id);
            return Ok(product);
        }
    }
}
