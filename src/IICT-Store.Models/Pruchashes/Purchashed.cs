using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Pruchashes
{
    public class Purchashed : BaseModel
    {
        public Purchashed()
        {
            CashMemos = new List<CashMemo>();
        }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public PurchaseStatus PurchaseStatus { get; set; }
        public PaymentProcess PaymentProcess { get; set; }
        public PaymentBy PaymentBy { get; set; }
        public ICollection<CashMemo> CashMemos { get; set; }
        //confisued
        // public int Purchasedby { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmedBy { get; set; }
        public string RejectedBy { get; set; }
        public DateTime PuchasedDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        public string Note { get; set; }
    }
}
