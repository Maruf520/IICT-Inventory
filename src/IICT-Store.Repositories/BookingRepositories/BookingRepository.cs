using IICT_Store.Models;
using IICT_Store.Models.Gallery;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.BookingRepositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        private readonly IICT_StoreDbContext context;
        public BookingRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Booking>> GetByDate(DateTime date)
        {
            var booking = await context.Bookings.Include(x => x.BookingTimeSlots).Where(x => x.Date.Date == date.Date).ToListAsync();
            return booking;
        }
    }
}
