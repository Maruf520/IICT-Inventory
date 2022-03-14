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
        Task<ServiceResponse<GetProductDto>> CreateProduct(CreateProductDto createProductDto, string userId);
        Task<ServiceResponse<GetProductDto>> GetProductById(long id);
        Task<ServiceResponse<List<GetProductDto>>> GetALlProduct();
        Task<ServiceResponse<List<GetProductDto>>> GetProductByCategoryId(long id);
        Task<ServiceResponse<CreateProductDto>> UpdateProduct(CreateProductDto createProductDto, long productId, string userId);
        Task<ServiceResponse<GetProductDto>> DeleteProduct( long productId);
        Task<ServiceResponse<GetProductDto>> InsertProductNo(long id, CreateProductNoDto createProductNoDto, string userId);
        Task<ServiceResponse<GetProductDto>> GetProductBySerialNo(long serialNo);
        Task<ServiceResponse<List<GetProductNoDto>>> GetAllAvailableProductno(long productId);
        Task<ServiceResponse<GetProductNoDto>> ReturnProductToStore(long productNo, string userId);

        Task<ServiceResponse<GetProductDto>> InsertProductNoMultiple(long id, FileUploadDto fileUploadDto,
            string userId);
    }
}
