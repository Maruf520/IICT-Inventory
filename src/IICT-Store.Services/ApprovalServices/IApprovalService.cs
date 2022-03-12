using IICT_Store.Dtos.Purchases;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ApprovalServices
{
    public interface IApprovalService
    {
        Task<ServiceResponse<List<GetPurchaseDto>>> GetPendingPurchase();
        public  Task<ServiceResponse<List<GetPurchaseDto>>> GetRejectedPurchase();
        public  Task<ServiceResponse<List<GetPurchaseDto>>> GetConfirmedPurchase();
        public  Task<ServiceResponse<GetPurchaseDto>> ConfirmStatus(long id, string userId);
        public  Task<ServiceResponse<GetPurchaseDto>> RejectStatus(long id, string userId);
        Task<ServiceResponse<GetPurchaseDto>> GetById(long id);

    }
}
