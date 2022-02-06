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
            booking.Date = createBookingDto.Date;
            booking.Note = createBookingDto.Note;
            booking.Purposes = createBookingDto.Purposes;
            booking.CreatedAt = DateTime.Now;
            bookingRespository.Insert(booking);
            foreach(var slotId in createBookingDto.TimeSlodId)
            {
                BookingTimeSlot bookingTimeSLot = new();
                bookingTimeSLot.TimeSlotId = slotId;
                bookingTimeSLot.BookingId = booking.Id;
                bookingTimeSLot.Date = createBookingDto.Date;
                bookingTimeSlotRepository.Insert(bookingTimeSLot);
            }
            response.Messages.Add("Booked.");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }

        public async Task<ServiceResponse<List<GetTimeSlotDto>>> GetAvailableTimeSlot(DateTime date)
        {

            ServiceResponse<List<GetTimeSlotDto>> response = new();
            List<GetTimeSlotDto> getTimeSlotDtos = new();
            var bookingTImeslots =  bookingTimeSlotRepository.GetByDate(date).Result;
            if(bookingTImeslots == null)
            {
                response.Messages.Add("No Booking Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var timeslots =  timeSlotRepository.GetAll().ToList();
            foreach(var timeslot  in timeslots.ToList() )
            {
                foreach(var bookingtimeslot in bookingTImeslots)
                {
                    if(timeslot.Id == bookingtimeslot.TimeSlotId)
                    {
                        var filter = timeslots.FirstOrDefault(x => x.Id == timeslot.Id);
                        timeslots.Remove(filter);
                    }
                }
            }
            var map = mapper.Map<IEnumerable<GetTimeSlotDto>>(timeslots);
            response.Data = (List<GetTimeSlotDto>)map;
            response.Messages.Add("All available Time Slots.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetBookingDto>>> GetBookingByDate(DateTime date)
        {
            ServiceResponse<List<GetBookingDto>> response = new();
            var booking = await bookingRespository.GetByDate(date);
            if( booking == null)
            {
                response.Messages.Add("No Booking Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var map = mapper.Map<IEnumerable<GetBookingDto>>(booking);
            response.Data = (List<GetBookingDto>)map;
            response.Messages.Add("All booking.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<ServiceResponse<GetBookingDto>> GetById(long id)
        {
            ServiceResponse<GetBookingDto> response = new();
            var booking = bookingRespository.GetById(id);
            if(booking == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var map = mapper.Map<GetBookingDto>(booking);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Booking.");
            response.Data = map;
            return response;
        }

    }
}
