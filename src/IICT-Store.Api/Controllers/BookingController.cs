using IICT_Store.Dtos.Gallery;
using IICT_Store.Models.Gallery;
using IICT_Store.Services.BookingServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : BaseController
    {

        private readonly IBookingService bookingService;
        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }
       // [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] CreateBookingDto createBookingDto)
        {
            var booking = await bookingService.CreateBooking(createBookingDto, GetuserId());
            return Ok(booking);
        }
       // [Authorize(Roles = "User")]
        [HttpGet("{date}/booking")]
        public async Task<IActionResult> GetBookingByDate(DateTime date, GalleryNo galleryNo)
        {
            var booking = await bookingService.GetCombinedBookingByDate(date, galleryNo);
            return Ok(booking);
        }
       // [Authorize(Roles = "User")]
        [HttpGet("{id}/")]
        public async Task<IActionResult> GetBookingById(long id)
        {
            var booking = await bookingService.GetById(id);
            return Ok(booking);
        }
      //  [Authorize(Roles = "User")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable(DateTime date, GalleryNo galleryNo)
        {
            var booking = await bookingService.GetAvailableTimeSlot(date, galleryNo);
            return Ok(booking);
        }
       // [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var booking = await bookingService.Delete(id);
            return Ok(booking);
        }



    }
}
