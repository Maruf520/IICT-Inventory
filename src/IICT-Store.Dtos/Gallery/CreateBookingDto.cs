using IICT_Store.Models.Gallery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Gallery
{
    public class CreateBookingDto
    {
        public GalleryNo GalleryNo { get; set; }
        public IFormFile Application { get; set; }
        public DateTime Date { get; set; }
        public List<int> TimeSlodId { get; set; }
        public Decimal Amount { get; set; }
        public IFormFile MoneyReceipt { get; set; }
        public string MoneyReceiptNo { get; set; }
        public string BookingBy { get; set; }
        public string Purposes { get; set; }
        public string Note { get; set; }
    }
}
