using AutoMapper;
using IICT_Store.Dtos.Categories;
using IICT_Store.Models;
using IICT_Store.Models.Categories;
using IICT_Store.Repositories.CategoryRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICT_Store.Repositories.TestRepo;

namespace IICT_Store.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IBaseRepo repo;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IBaseRepo repo)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.repo = repo;
        }

        public async Task<ServiceResponse<GetCategoryDto>> CreateCategory(CategoryDto categoryDto, string userId)
        {
            ServiceResponse<GetCategoryDto> response = new();
            Category category = new();
            var categories = categoryRepository.GetAll();
            if (categories.Any(x => x.Name == categoryDto.Name))
            {
                response.SetMessage(new List<string>{new ("Category already exists.")});
                return response;
            }
            var uploadImage = "";
            if(categoryDto.Image != null)
            {
                uploadImage = await UploadImage(categoryDto.Image);
            }
            category.Name = categoryDto.Name;
            category.CreatedAt = DateTime.Now;
            category.Description = categoryDto.Discription;
            category.Image = uploadImage;
            category.CreatedBy = userId;
            categoryRepository.Insert(category);
            var categoryToMap = mapper.Map<GetCategoryDto>(category);
            response.Messages.Add("Category Added.");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            response.Data = categoryToMap;
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> DeleteCategory(long id)
        {
            ServiceResponse<GetCategoryDto> response = new();
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                response.Messages.Add("Nit Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            categoryRepository.Delete(id);
            var categoryToMap = mapper.Map<GetCategoryDto>(category);
            response.Data = categoryToMap;
            response.Messages.Add("Deleted");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(long id)
        {
            ServiceResponse<GetCategoryDto> response = new();
            // var category = categoryRepository.GetById(id);
            var category = repo.GetItems<Category>(e => e.Id == id).FirstOrDefault();
            if(category == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var categoryToMap = mapper.Map<GetCategoryDto>(category);
            response.StatusCode = System.Net.HttpStatusCode.OK;;
            response.Data = categoryToMap;
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(CategoryDto categoryDto, long id, string userId)
        {
            ServiceResponse<GetCategoryDto> response = new();
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                response.Messages.Add("Not Found");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            category.UpdatedBy = userId;
            category.UpdatedAt = DateTime.Now;
            category.Name = categoryDto.Name;
            categoryRepository.Update(category);
            var categoryToMap = mapper.Map<GetCategoryDto>(category);
            response.Data = categoryToMap;
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

        public async Task<string> UploadImage(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                string fName = Path.GetRandomFileName();

                var getext = Path.GetExtension(formFile.FileName);
                var filename = Path.ChangeExtension(fName, getext);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, filename);
                var pathdb = "files/" + filename;
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                    stream.Flush();
                }

                return pathdb;

            }
            return "enter valid photo";
        }
    }
}
