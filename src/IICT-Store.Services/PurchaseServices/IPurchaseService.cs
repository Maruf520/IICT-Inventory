using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.Purchases;
using IICT_Store.Models;
using IICT_Store.Models.Pruchashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.PurchaseServices
{
    public interface IPurchaseService
    {
        Task<ServiceResponse<GetPurchaseDto>> CreatePurchase(CreatePurchasedDto createPurchaseDto, string userId);
        Task<ServiceResponse<GetPurchaseDto>> UpdatePurchase(CreatePurchasedDto createPurchaseDto, long id, string userId);
        Task<ServiceResponse<GetPurchaseDto>> GetPurchaseById(long id);
        Task<ServiceResponse<List<GetPurchaseDto>>> GetPurchaseByProductId(long id);
        Task<ServiceResponse<List<GetPurchaseDto>>> GetPurchaseByDate(int year, PaymentBy paymentBy, PaymentProcess paymentProcess);
        Task<ServiceResponse<List<GetPurchaseHistory>>> GetPurchaseHistory(int year, int productId);
    }
}
