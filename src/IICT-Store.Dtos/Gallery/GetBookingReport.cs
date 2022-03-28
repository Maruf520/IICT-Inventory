using System;

namespace IICT_Store.Dtos.Gallery
{
    public class GetBookingReport
    {
        public DateTime Date { get; set; }
        public string GalleryNo { get; set; }
        public string BookingBy { get; set; }
        public string Purposes { get; set; }
        public decimal Amount { get; set; }
        public string MoneyReceiptNo { get; set; }
    }
}