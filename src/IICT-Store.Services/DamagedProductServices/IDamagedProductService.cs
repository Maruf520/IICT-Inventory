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
        Task<ServiceResponse<ProductSerialNoDto>> DamageProduct(long id);
        Task<ServiceResponse<List<DamagedProductDto>>> GetAllDamagedProduct();
    }
}
