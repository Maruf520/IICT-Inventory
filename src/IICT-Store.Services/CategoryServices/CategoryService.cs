using AutoMapper;
using IICT_Store.Dtos.Categories;
using IICT_Store.Models;
using IICT_Store.Models.Categories;
using IICT_Store.Repositories.CategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            ServiceResponse<CategoryDto> response = new();
            Category category = new();
            category.Name = categoryDto.Name;
            category.CreatedAt = DateTime.Now;
            categoryRepository.Insert(category);
            response.Messages.Add("Category Added.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Data = categoryDto;
            return response;
        }

        public async Task<ServiceResponse<CategoryDto>> DeleteCategory(long id)
        {
            ServiceResponse<CategoryDto> response = new();
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                response.Messages.Add("Nit Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            categoryRepository.Delete(id);
            response.Messages.Add("Deleted");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<CategoryDto>> GetCategoryById(long id)
        {
            ServiceResponse<CategoryDto> response = new();
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            response.StatusCode = System.Net.HttpStatusCode.OK;
            CategoryDto categoryDto = new();
            categoryDto.Name = category.Name;
            response.Data = categoryDto;
            return response;
        }

        public async Task<ServiceResponse<CategoryDto>> UpdateCategory(CategoryDto categoryDto, long id)
        {
            ServiceResponse<CategoryDto> response = new();
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                response.Messages.Add("Not Found");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            
            category.UpdatedAt = DateTime.Now;
            category.Name = categoryDto.Name;
            categoryRepository.Update(category);
            response.Data = categoryDto;
            response.Messages.Add("Updated.");
            return response;
            
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategory()
        {
            ServiceResponse<List<GetCategoryDto>> response = new();
            var categories =  categoryRepository.GetAll().ToList();
            var categoryToMap = mapper.Map<List<GetCategoryDto>>(categories);
            response.Data = categoryToMap;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("All Categores.");
            return response;

        }
    }
}
