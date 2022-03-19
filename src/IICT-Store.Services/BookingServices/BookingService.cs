﻿using AutoMapper;
using IICT_Store.Dtos.Gallery;
using IICT_Store.Models;
using IICT_Store.Models.Gallery;
using IICT_Store.Repositories.BookingRepositories;
using IICT_Store.Repositories.BookingTimeSlotRepositories;
using IICT_Store.Repositories.TimeSlotRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<ServiceResponse<GetBookingDto>> CreateBooking(CreateBookingDto createBookingDto, string userId)
        {
            ServiceResponse<GetBookingDto> response = new();
            Booking booking = new();
            var application = await UploadFile(createBookingDto.Application);
            booking.Application = application;
            booking.BookingBy = createBookingDto.BookingBy;
            booking.Date = createBookingDto.Date;
            booking.Note = createBookingDto.Note;
            booking.Purposes = createBookingDto.Purposes;
            booking.CreatedAt = DateTime.Now;
            booking.GalleryNo = createBookingDto.GalleryNo;
            booking.Amount = createBookingDto.Amount;
            booking.CreatedBy = userId;
            booking.MoneyReceiptNo = createBookingDto.MoneyReceiptNo;
            if (createBookingDto.MoneyReceipt.Length > 0)
            {
                booking.MoneyReceipt = await UploadFile(createBookingDto.MoneyReceipt);
            }
            bookingRespository.Insert(booking);
            foreach(var slotId in createBookingDto.TimeSlotId)
            {
                BookingTimeSlot bookingTimeSLot = new();
                bookingTimeSLot.TimeSlotId = slotId;
                bookingTimeSLot.BookingId = booking.Id;
                bookingTimeSLot.GalleryNo = createBookingDto.GalleryNo;
                bookingTimeSLot.Date = createBookingDto.Date;
                bookingTimeSlotRepository.Insert(bookingTimeSLot);
            }
            response.Messages.Add("Booked.");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }

        public async Task<ServiceResponse<GetBookingDto>> Delete(long id)
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
            bookingRespository.Delete(id);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Deleted.");
            response.Data = map;
            return response;
        }

        public async Task<ServiceResponse<List<GetTimeSlotDto>>> GetAvailableTimeSlot(DateTime date,GalleryNo galleryNo)
        {

            ServiceResponse<List<GetTimeSlotDto>> response = new();
            List<GetTimeSlotDto> getTimeSlotDtos = new();
            var bookingTImeslots =  bookingTimeSlotRepository.GetByDate(date,galleryNo).Result;
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

        public async Task<ServiceResponse<List<GetBookingDto>>> GetBookingByDate(DateTime date, GalleryNo galleryNo)
        {
            ServiceResponse<List<GetBookingDto>> response = new();
            var booking = await bookingRespository.GetByDate(date, galleryNo);

            
            if ( booking.Count == 0)
            {
                response.Messages.Add("No Booking Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
           // List<GetTimeSlotDto> timeSlotDtos = new();
            List < GetBookingDto > GetBookingDtos = new();
            foreach(var book in booking)
            {
                GetBookingDto getBookingDto = new();
                getBookingDto.Id = book.Id;
                getBookingDto.Application = book.Application;
                getBookingDto.BookingBy = book.BookingBy;
                getBookingDto.Date = book.Date;
                getBookingDto.Purposes = book.Purposes;
                getBookingDto.Note = book.Note;
                GetBookingDtos.Add(getBookingDto);
                List<GetTimeSlotDto> timeSlotDtos = new();
                foreach (var timeslotId in book.BookingTimeSlots)
                {
                    var timeSlot =  timeSlotRepository.GetById(timeslotId.Id);
                    GetTimeSlotDto getTimeSlotDto = new();
                    getTimeSlotDto.Id = timeslotId.Id;
                    getTimeSlotDto.StartTime = timeSlot.StartTime;
                    getTimeSlotDto.EndTime = timeSlot.EndTime;
                    timeSlotDtos.Add(getTimeSlotDto);

                }
                getBookingDto.BookingTimeSlots = timeSlotDtos;
            }
            response.Data = GetBookingDtos;
            response.Messages.Add("All booking.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }
        public async Task<ServiceResponse<List<CombinedTimeSlotDto>>> GetCombinedBookingByDate(DateTime date, GalleryNo galleryNo)
        {
            ServiceResponse<List<CombinedTimeSlotDto>> response = new();
            List<CombinedTimeSlotDto> combinedTimeSlotDtos = new();
            var bookingTImeslots = bookingTimeSlotRepository.GetByDate(date, galleryNo).Result;
            if (bookingTImeslots == null)
            {
                response.Messages.Add("No Booking Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var timeslots = timeSlotRepository.GetAll().ToList();
            foreach (var timeslot in timeslots.ToList())
            {
                foreach (var bookingtimeslot in bookingTImeslots)
                {
                    if (timeslot.Id == bookingtimeslot.TimeSlotId)
                    {
                        var filter = timeslots.FirstOrDefault(x => x.Id == timeslot.Id);
                        timeslots.Remove(filter);
                    }
                }
            }
            foreach(var time in timeslots)
            {
                CombinedTimeSlotDto combinedTimeSlotDto = new();
                combinedTimeSlotDto.TimeSlotId = time.Id;
                combinedTimeSlotDto.StartTime = time.StartTime;
                combinedTimeSlotDto.EndTime = time.EndTime;
                combinedTimeSlotDtos.Add(combinedTimeSlotDto);
            }
            var booking = await bookingRespository.GetByDate(date, galleryNo);
            foreach(var book in booking)
            {
                foreach(var timeslotId in book.BookingTimeSlots)
                {
                    CombinedTimeSlotDto combinedTimeSlotDto1 = new();
                    GetBookingDto getBookingDto = new();
                    combinedTimeSlotDto1.BookingId = book.Id;
                    combinedTimeSlotDto1.Application = book.Application;
                    combinedTimeSlotDto1.BookingBy = book.BookingBy;
                    combinedTimeSlotDto1.Date = book.Date;
                    combinedTimeSlotDto1.Purposes = book.Purposes;
                    combinedTimeSlotDto1.Note = book.Note;
                    combinedTimeSlotDto1.Amount = book.Amount;
                    combinedTimeSlotDto1.MoneyReceiptNo = book.MoneyReceiptNo;
                    combinedTimeSlotDto1.MoneyReceipt = book.MoneyReceipt;
                    var timeSlot = timeSlotRepository.GetById(timeslotId.TimeSlotId);
                    combinedTimeSlotDto1.StartTime = timeSlot.StartTime;
                    combinedTimeSlotDto1.TimeSlotId = timeSlot.Id;
                    combinedTimeSlotDto1.EndTime = timeSlot.EndTime;
                    combinedTimeSlotDtos.Add(combinedTimeSlotDto1);
                }
            }

            response.Data = combinedTimeSlotDtos;
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

        public async Task<string> UploadFile(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                string fName = Path.GetRandomFileName();

                var getext = Path.GetExtension(formFile.FileName);
                var filename = Path.ChangeExtension(fName, getext);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, filename);
                var pathdb = "files/" + filename;
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                    stream.Flush();
                }

                return pathdb;

            }
            return "enter valid photo";
        }
    }
}
