using IICT_Store.Models.Products;
using IICT_Store.Models.Pruchashes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Purchases
{
    public class CreatePurchasedDto
    {
        public List<IFormFile> File { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public PaymentBy PaymentBy { get; set; }
        public string Description { get; set; }
        [Required]
        public string Supplier { get; set; }
        /*        public PurchaseStatus PurchaseStatus { get; set; }*/
        [Required]
        public PaymentProcess PaymentProcess { get; set; }
        //confisued
        //[Required]
        //public int Purchasedby { get; set; }
        /*        public int ConfirmedBy { get; set; }*/
        [Required]
        public DateTime PuchasedDate { get; set; }
        /*        public DateTime ConfirmDate { get; set; }*/
        public string Note { get; set; }
    }
}
