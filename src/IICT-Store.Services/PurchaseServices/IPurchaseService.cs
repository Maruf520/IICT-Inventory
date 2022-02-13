using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.Purchases;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.PurchaseServices
{
    public interface IPurchaseService
    {
        Task<ServiceResponse<GetPurchaseDto>> CreatePurchase(CreatePurchasedDto createPurchaseDto);
        Task<ServiceResponse<GetPurchaseDto>> UpdatePurchase(CreatePurchasedDto createPurchaseDto, long id);
        Task<ServiceResponse<GetPurchaseDto>> GetPurchaseById(long id);
        Task<ServiceResponse<List<GetPurchaseDto>>> GetPurchaseByProductId(long id);
    }
}
