using IICT_Store.Models.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.BookingRepositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>> GetByDate(DateTime date);
    }
}
