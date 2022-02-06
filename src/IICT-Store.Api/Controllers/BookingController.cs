using IICT_Store.Dtos.Gallery;
using IICT_Store.Services.BookingServices;
using IICT_Store.Services.TimeSlotServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingService bookingService;
        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            var booking = await bookingService.CreateBooking(createBookingDto);
            return Ok(booking);
        }

        [HttpGet("{date}/booking")]
        public async Task<IActionResult> GetBookingByDate(DateTime date)
        {
            var booking = await bookingService.GetBookingByDate(date);
            return Ok(booking);
        }
        [HttpGet("{id}/")]
        public async Task<IActionResult> GetBookingById(long id)
        {
            var booking = await bookingService.GetById(id);
            return Ok(booking);
        }

        [HttpGet("available-booking")]
        public async Task<IActionResult> GetAvailableBooking(DateTime date)
        {
            var booking = await bookingService.GetAvailableTimeSlot(date);
            return Ok(booking);
        }



    }
}
