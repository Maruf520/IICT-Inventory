using IICT_Store.Models.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.BookingTimeSlotRepositories
{
    public interface IBookingTimeSlotRepository : IBaseRepository<BookingTimeSlot>
    {
        Task<List<BookingTimeSlot>> GetByDate(DateTime date, GalleryNo galleryNo);
    }
}
