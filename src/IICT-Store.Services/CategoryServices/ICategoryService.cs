using IICT_Store.Dtos.Categories;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryDto>> CreateCategory(CategoryDto categoryDto);
        Task<ServiceResponse<CategoryDto>> GetCategoryById(long id);
        Task<ServiceResponse<CategoryDto>> UpdateCategory(CategoryDto categoryDto,long id);
        Task<ServiceResponse<CategoryDto>> DeleteCategory(long id);
        Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategory();
    }
}
