using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Services.ProductNumberServices;
using IICT_Store.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
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
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly IProductNumberService productNumberService;
        public ProductController(IProductService productService, IProductNumberService productNumberService)
        {
            this.productService = productService;
            this.productNumberService = productNumberService;
        }
        //[Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
        {
            var product = await productService.CreateProduct(createProductDto, GetuserId());
            return Ok(product);
        }
        //[Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAction(long id)
        {
            var product = await productService.GetProductById(id);
            return Ok(product);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await productService.GetALlProduct();
            return Ok(products);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetProdctByCategoryId(long id)
        {
            var product = await productService.GetProductByCategoryId(id);
            return Ok(product);
        }
        //[Authorize(Roles = "User, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(CreateProductDto createProductDto, long id)
        {
            var product = await productService.UpdateProduct(createProductDto, id, GetuserId());
            return Ok(product);
        }
        //[Authorize(Roles = "User, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeteteProduct(long id)
        {
            var product = await productService.DeleteProduct(id);
            return Ok(product);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpPost("{id}/serial")]
        public async Task<IActionResult> InsertProductNo(long id, CreateProductNoDto createProductNoDto)
        {
            var product = await productNumberService.InsertProductNo(id, createProductNoDto, GetuserId());
            return Ok(product);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpGet("serial/{id}")]
        public async Task<IActionResult> GetProductBySerialNo(long id)
        {
            var serial = await productNumberService.GetProductNoByProductId(id);
            return Ok(serial);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpGet("available/{id}")]
        public async Task<IActionResult> GetAvailableProduct(long id)
        {
            var product = await productService.GetAllAvailableProductno(id);
            return Ok(product);
        }
        //[Authorize(Roles = "User, Admin")]
        [HttpGet("return/{id}")]
        public async Task<IActionResult> ReturnTOStock(long id)
        {
            var product = await productService.ReturnProductToStore(id, GetuserId());
            return Ok(product);
        }
        // 
        [HttpPost("{id}/multiple-serial")]
        public async Task<IActionResult> AddMultipleSerial(long id,[FromForm] FileUploadDto fileDto)
        {
            var file = await productService.InsertProductNoMultiple(id, fileDto, GetuserId());
            return Ok(file);
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetProductReport(int year)
        {
            var report = await productService.GetProductDetaills(year);
            return Ok(report);
        }
    }
}
