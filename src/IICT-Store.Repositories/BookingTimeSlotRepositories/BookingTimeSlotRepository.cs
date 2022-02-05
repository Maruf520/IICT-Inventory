using IICT_Store.Models;
using IICT_Store.Models.Gallery;
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
    }
}
