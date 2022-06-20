using IICT_Store.Models;
using IICT_Store.Models.Gallery;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.BookingTimeSlotRepositories
{
    public class BookingTimeSlotRepository : BaseRepository<BookingTimeSlot>, IBookingTimeSlotRepository
    {
        private readonly IICT_StoreDbContext context;
        public BookingTimeSlotRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<BookingTimeSlot>> GetByDate(DateTime date, GalleryNo galleryNo)
        {
            var timeSlots = await context.BookingTimeSlots.Include(x => x.Booking).Where(x => x.Date.Date == date.Date && x.GalleryNo == galleryNo).ToListAsync();
            return timeSlots;
        }

        public async Task<BookingTimeSlot> GetByBookingId(long id)
        {
            var timeSlot = await context.BookingTimeSlots.Where(x => x.BookingId == id).FirstOrDefaultAsync();
            return timeSlot;
        }
    }
}
