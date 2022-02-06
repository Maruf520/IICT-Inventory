using IICT_Store.Models.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Gallery
{
    public class GetBookingDto
    {
        public string Application { get; set; }
        public DateTime Date { get; set; }
        public ICollection<BookingTimeSlot> BookingTimeSlots { get; set; }
        public string BookingBy { get; set; }
        public string Purposes { get; set; }
        public string Note { get; set; }
    }
}
