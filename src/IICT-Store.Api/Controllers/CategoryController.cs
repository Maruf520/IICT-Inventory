using IICT_Store.Dtos.Categories;
using IICT_Store.Services.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CategoryDto categoryDto)
        {
            var category = await categoryService.CreateCategory(categoryDto,GetuserId());
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategryById(long id)
        {
            var category = await categoryService.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromForm]CategoryDto categoryDto, long id)
        {
            var category = await categoryService.UpdateCategory(categoryDto, id, GetuserId());
            return Ok(category);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await categoryService.DeleteCategory(id);
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryService.GetAllCategory();
            return Ok(categories);
        }
    }
}
