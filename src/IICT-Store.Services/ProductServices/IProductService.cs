using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<GetProductDto>> CreateProduct(CreateProductDto createProductDto);
        Task<ServiceResponse<GetProductDto>> GetProductById(long id);
        Task<ServiceResponse<List<GetProductDto>>> GetALlProduct();
        Task<ServiceResponse<CreateProductDto>> UpdateProduct(CreateProductDto createProductDto, long productId);
        Task<ServiceResponse<GetProductDto>> DeleteProduct( long productId);
    }
}
