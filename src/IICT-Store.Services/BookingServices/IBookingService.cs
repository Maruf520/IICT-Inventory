using IICT_Store.Dtos.Gallery;
using IICT_Store.Models;
using IICT_Store.Models.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.BookingServices
{
    public interface IBookingService
    {
        Task<ServiceResponse<GetBookingDto>> CreateBooking(CreateBookingDto createBookingDto, string userId);
        Task<ServiceResponse<GetBookingDto>> GetById(long id);
        Task<ServiceResponse<GetBookingDto>> Delete(long id);
        Task<ServiceResponse<List<GetBookingDto>>> GetBookingByDate(DateTime date, GalleryNo galleryNo);
        Task<ServiceResponse<List<GetTimeSlotDto>>> GetAvailableTimeSlot(DateTime date, GalleryNo galleryNo);
        Task<ServiceResponse<List<CombinedTimeSlotDto>>> GetCombinedBookingByDate(DateTime date, GalleryNo galleryNo);
        Task<ServiceResponse<List<GetBookingReport>>> GetReport(DateTime start, DateTime end);

    }
}
