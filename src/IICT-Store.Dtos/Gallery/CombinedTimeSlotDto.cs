using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Gallery
{
    public class CombinedTimeSlotDto
    {
        public long TimeSlotId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public long BookingId { get; set; }
        public string Application { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string MoneyReceipt { get; set; }
        public string MoneyReceiptNo { get; set; }
        public string BookingBy { get; set; }
        public string Purposes { get; set; }
        public string Note { get; set; }
    }
}
