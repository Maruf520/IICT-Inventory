using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Gallery
{
    public class BookingTimeSlot : BaseModel
    {
        public long BookingId { get; set; }
        public Booking Booking { get; set; }
        public long TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}
