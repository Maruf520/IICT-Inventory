using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Gallery
{
    public class TimeSlot : BaseModel
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public ICollection<BookingTimeSlot> BookingTimeSlots { get; set; }
    }
}
