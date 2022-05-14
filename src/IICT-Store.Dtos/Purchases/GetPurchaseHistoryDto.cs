using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models.Pruchashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Purchases
{
    public class GetPurchaseHistoryDto
    {
        public long Id { get; set; }
        public GetProductDto Product { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
        public PaymentProcess PaymentProcess { get; set; }
        public PaymentBy PaymentBy { get; set; }
        public string Description { get; set; }
        public DateTime PuchasedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Note { get; set; }
    }
}
