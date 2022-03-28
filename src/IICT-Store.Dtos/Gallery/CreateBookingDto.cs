using IICT_Store.Models.Gallery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Gallery
{
    public class CreateBookingDto
    {
        [Required]
        public GalleryNo GalleryNo { get; set; }
        [Required]
        public IFormFile Application { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public List<int> TimeSlotId { get; set; }
        public Decimal Amount { get; set; }
        public IFormFile MoneyReceipt { get; set; }

        public string MoneyReceiptNo { get; set; }
        [Required]
        public string BookingBy { get; set; }
        [Required]
        public string Purposes { get; set; }
        public string Note { get; set; }
    }
}
