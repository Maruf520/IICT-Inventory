using AutoMapper;
using IICT_Store.Dtos.Gallery;
using IICT_Store.Models;
using IICT_Store.Models.Gallery;
using IICT_Store.Repositories.TimeSlotRepository;
using IICT_Store.Services.TimeSlotServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.TimeSlotService
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotReposiotry timeSlotReposiotry;
        private readonly IMapper mapper;
        public TimeSlotService(ITimeSlotReposiotry timeSlotReposiotry, IMapper mapper)
        {
            this.timeSlotReposiotry = timeSlotReposiotry;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<GetTimeSlotDto>> CreaateTimeSlot(CreateTimeSlotDto createTimeSlotDto, string userId)
        {
            ServiceResponse<GetTimeSlotDto> response = new();
            var timeSlots = timeSlotReposiotry.GetAll();
            if(timeSlots == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            foreach(var timeSlot in timeSlots)
            {
                if(createTimeSlotDto.StartTime == timeSlot.StartTime && createTimeSlotDto.EndTime == timeSlot.EndTime)
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Messages.Add("Time slot already exists.");
                    return response;
                }
            }
           var  timeSlotToCreate = mapper.Map<TimeSlot>(createTimeSlotDto);
           timeSlotToCreate.CreatedAt = DateTime.Now;
           timeSlotToCreate.CreatedBy = userId;
            var timeslotToReturn = mapper.Map<GetTimeSlotDto>(createTimeSlotDto);
            timeSlotReposiotry.Insert(timeSlotToCreate);
            response.StatusCode = System.Net.HttpStatusCode.Created;
            response.Messages.Add("Created.");
            response.Data = timeslotToReturn;
            return response;
        }

        public async Task<ServiceResponse<GetTimeSlotDto>> Delete(long id)
        {
            ServiceResponse<GetTimeSlotDto> response = new();
            var timeSlot =  timeSlotReposiotry.GetById(id);
            if(timeSlot == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            timeSlotReposiotry.Delete(id);
            var map = mapper.Map<GetTimeSlotDto>(timeSlot);
            response.Data = map;
            response.Messages.Add("Deleted.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetTimeSlotDto>>> GetAll()
        {
            ServiceResponse<List<GetTimeSlotDto>> response = new();
            var timeSlots = timeSlotReposiotry.GetAll();
            if (timeSlots == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var timeslotToReturn = mapper.Map<List<GetTimeSlotDto>>(timeSlots);
  
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("All Time Slot.");
            response.Data = timeslotToReturn;
            return response;
        }

        public async Task<ServiceResponse<GetTimeSlotDto>> GetById(long id)
        {
            ServiceResponse<GetTimeSlotDto> response = new();
            var timeSlot = timeSlotReposiotry.GetById(id);
            if (timeSlot == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var map = mapper.Map<GetTimeSlotDto>(timeSlot);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Time Slot.");
            response.Data = map;
            return response;
        }

        public async Task<ServiceResponse<GetTimeSlotDto>> Update(CreateTimeSlotDto createTimeSlotDto, long id, string userId)
        {
            ServiceResponse<GetTimeSlotDto> response = new();
            var timeSlot = timeSlotReposiotry.GetById(id);
            if (timeSlot == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var map = mapper.Map<TimeSlot>(createTimeSlotDto);
            map.UpdatedAt = DateTime.Now;
            map.UpdatedBy = userId;
            timeSlotReposiotry.Update(map);
            response.Messages.Add("Updated.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }
    }
}
