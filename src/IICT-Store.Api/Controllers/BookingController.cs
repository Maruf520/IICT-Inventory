using IICT_Store.Dtos.Gallery;
using IICT_Store.Models.Gallery;
using IICT_Store.Services.BookingServices;
using IICT_Store.Services.TimeSlotServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
       // [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] CreateBookingDto createBookingDto)
        {
            var booking = await bookingService.CreateBooking(createBookingDto);
            return Ok(booking);
        }
       // [Authorize(Roles = "User")]
        [HttpGet("{date}/booking")]
        public async Task<IActionResult> GetBookingByDate(DateTime date, GalleryNo galleryNo)
        {
            var booking = await bookingService.GetBookingByDate(date, galleryNo);
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
