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
        Task<ServiceResponse<GetCategoryDto>> CreateCategory(CategoryDto categoryDto);
        Task<ServiceResponse<GetCategoryDto>> GetCategoryById(long id);
        Task<ServiceResponse<GetCategoryDto>> UpdateCategory(CategoryDto categoryDto,long id);
        Task<ServiceResponse<GetCategoryDto>> DeleteCategory(long id);
        Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategory();
    }
}
