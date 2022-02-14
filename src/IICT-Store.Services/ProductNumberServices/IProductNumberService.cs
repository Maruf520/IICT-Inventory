using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ProductNumberServices
{
    public interface IProductNumberService
    {
        Task<ServiceResponse<GetProductDto>> InsertProductNo(long id, CreateProductNoDto createProductNoDto);
        Task<ServiceResponse<List<GetProductNoDto>>> GetProductNoByProductId(long productId);
    }
}
