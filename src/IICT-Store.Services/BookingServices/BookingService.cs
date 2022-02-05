using AutoMapper;
using IICT_Store.Dtos.Gallery;
using IICT_Store.Models;
using IICT_Store.Models.Gallery;
using IICT_Store.Repositories.BookingRepositories;
using IICT_Store.Repositories.BookingTimeSlotRepositories;
using IICT_Store.Repositories.TimeSlotRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IMapper mapper;
        private readonly ITimeSlotReposiotry timeSlotRepository;
        private readonly IBookingRepository bookingRespository;
        private readonly IBookingTimeSlotRepository bookingTimeSlotRepository;
        public BookingService(ITimeSlotReposiotry timeSlotRepository, IBookingRepository bookingRespository, IMapper mapper, IBookingTimeSlotRepository bookingTimeSlotRepository)
        {
            this.bookingRespository = bookingRespository;
            this.timeSlotRepository = timeSlotRepository;
            this.mapper = mapper;
            this.bookingTimeSlotRepository = bookingTimeSlotRepository;
        }

        public async Task<ServiceResponse<GetBookingDto>> CreateBooking(CreateBookingDto createBookingDto)
        {
            ServiceResponse<GetBookingDto> response = new();
            Booking booking = new();
            booking.Application = createBookingDto.Application;
            booking.BookingBy = createBookingDto.BookingBy;
            booking.Date = DateTime.Now;
            booking.Note = createBookingDto.Note;
            booking.Purposes = createBookingDto.Purposes;
            booking.CreatedAt = DateTime.Now;
/*            var map = mapper.Map<Booking>(createBookingDto);*/
/*            map.CreatedAt = DateTime.Now;*/
            bookingRespository.Insert(booking);
            foreach(var slotId in createBookingDto.TimeSlodId)
            {
                BookingTimeSlot bookingTimeSLot = new();
                bookingTimeSLot.TimeSlotId = slotId;
                bookingTimeSLot.BookingId = booking.Id;
                bookingTimeSlotRepository.Insert(bookingTimeSLot);
            }
            response.Messages.Add("Booked.");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }

    }
}
