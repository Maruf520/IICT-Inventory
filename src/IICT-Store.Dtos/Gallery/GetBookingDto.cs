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
        public long Id { get; set; }
        public string Application { get; set; }
        public DateTime Date { get; set; }
        public ICollection<GetTimeSlotDto> BookingTimeSlots { get; set; }
        public string BookingBy { get; set; }
        public string Purposes { get; set; }
        public string Note { get; set; }
    }
}
