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
        public long ProductId { get; set; }
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
        public int ConfirmedBy { get; set; }
        public DateTime PuchasedDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        public bool IsConfirmed { get; set; }
        public string Note { get; set; }
    }
}
