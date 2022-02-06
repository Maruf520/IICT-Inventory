using IICT_Store.Models;
using IICT_Store.Models.Gallery;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.TimeSlotRepository
{
    public class TimeSlotRepository : BaseRepository<TimeSlot>, ITimeSlotReposiotry
    {
        private readonly IICT_StoreDbContext context;
        public TimeSlotRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<BookingTimeSlot>> GetByBookingId(long id)
        {
            var timeslots = await context.BookingTimeSlots.Include(x => x.Booking).Where(x => x.TimeSlotId == id).ToListAsync();
            return timeslots;
        }
    }
}
