using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Services.ProductServices;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await productService.GetALlProduct();
            return Ok(products);
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
            var product = await productService.InsertProductNo(id, createProductNoDto);
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
