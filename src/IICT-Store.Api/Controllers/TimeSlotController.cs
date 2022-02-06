using IICT_Store.Dtos.Gallery;
using IICT_Store.Services.TimeSlotServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotService timeslotService;
        public TimeSlotController(ITimeSlotService timeslotService)
        {
            this.timeslotService = timeslotService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeSlot(CreateTimeSlotDto createTimeSlotDto)
        {
            var timeslot = await timeslotService.CreaateTimeSlot(createTimeSlotDto);
            return Ok(timeslot);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTimeSlot()
        {
            var timeSlot = await timeslotService.GetAll();
            return Ok(timeSlot);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllTimSlotById(long id)
        {
            var timeSlot = await timeslotService.GetById(id);
            return Ok(timeSlot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(CreateTimeSlotDto createTimeSlotDto, long id)
        {
            var timeSLot = await timeslotService.Update(createTimeSlotDto, id);
            return Ok(timeSLot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var timeSlot = await timeslotService.Delete(id);
            return Ok(timeSlot);
        }
    }
}
