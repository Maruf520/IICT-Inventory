using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Gallery
{
    public class Booking : BaseModel
    {
        public Booking()
        {
            BookingTimeSlots = new List<BookingTimeSlot>();
        }

        public GalleryNo GalleryNo { get; set; }
        public string Application { get; set; }
        public DateTime Date { get; set; }
        public ICollection<BookingTimeSlot> BookingTimeSlots { get; set; }
        public string BookingBy { get; set; }
        public string Purposes { get; set; }
        public decimal Amount { get; set; }
        public string MoneyReceipt { get; set; }
        public string MoneyReceiptNo { get; set; }
        public string Note { get; set; }
    }
}
