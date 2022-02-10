using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ReturnProductServices
{
    public interface IReturnProductService
    {
        Task<ServiceResponse<GetReturnProductDto>> CreateReturnProduct(CreateReturnProductDto createReturnProductDto, int id);
    }
}
