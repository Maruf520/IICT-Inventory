using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Gallery
{
    public class GetTimeSlotDto
    {
        public long Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTIme { get; set; }
    }
}
