using IICT_Store.Dtos.Gallery;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.BookingServices
{
    public interface IBookingService
    {
        Task<ServiceResponse<GetBookingDto>> CreateBooking(CreateBookingDto createBookingDto);
    }
}
