using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models.Pruchashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Purchases
{
    public class GetPurchaseDto
    {
        public long Id { get; set; }
        public GetProductDto Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
        public ICollection<CashMemoDtos> CashMemos { get; set; }
        public PurchaseStatus PurchaseStatus { get; set; }
        public PaymentProcess PaymentProcess { get; set; }
        public PaymentBy PaymentBy { get; set; }
        public string Description { get; set; }
        //confisued
        public int Purchasedby { get; set; }
        public string ConfirmedBy { get; set; }
        public DateTime PuchasedDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public GetUserDto CreatedByUser { get; set; }
        public GetUserDto ConfirmByUser { get; set; }
        public GetUserDto RejectedByUser { get; set; }
        public bool IsConfirmed { get; set; }
        public string Note { get; set; }
    }
}
