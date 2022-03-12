using IICT_Store.Dtos.Gallery;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.TimeSlotServices
{
    public interface ITimeSlotService
    {
        Task<ServiceResponse<GetTimeSlotDto>>CreaateTimeSlot(CreateTimeSlotDto createTimeSlotDto, string userId);
         Task<ServiceResponse<List<GetTimeSlotDto>>> GetAll();
         Task<ServiceResponse<GetTimeSlotDto>> Delete(long id);
         Task<ServiceResponse<GetTimeSlotDto>> Update(CreateTimeSlotDto createTimeSlotDto,long id, string userId);
         Task<ServiceResponse<GetTimeSlotDto>> GetById(long id);

    }
}
