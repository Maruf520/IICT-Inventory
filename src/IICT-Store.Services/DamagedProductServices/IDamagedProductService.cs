using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.DamagedProductServices
{
    public interface IDamagedProductService
    {
        Task<ServiceResponse<DamagedProductDto>> DamageProduct(CreateDamagedProductDto damagedProduct, string userId);
        Task<ServiceResponse<List<DamagedProductDto>>> GetAllDamagedProduct();
        Task<ServiceResponse<List<GetDamagedProductDto>>> GetDamagedProductByProductId(long id);
        Task<ServiceResponse<GetDamagedProductDto>> GetDamagedProductProductNoId(long id);
    }
}
