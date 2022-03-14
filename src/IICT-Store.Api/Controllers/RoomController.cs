using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IICT_Store.Dtos.Rooms;
using IICT_Store.Services.RoomServices;

namespace IICT_Store.Api.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : BaseController
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [HttpPost]
        public  IActionResult CreateRoom([FromForm]CreateRoomDto createRoomDto)
        {
            var room = roomService.CreateRoom(createRoomDto, GetuserId());
            return Ok(room);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = roomService.GetAllRoom();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = roomService.GetById(id);
            return Ok(room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom([FromForm]CreateRoomDto createRoomDto, int id)
        {
            var room = roomService.UpdateRoom(createRoomDto, id, GetuserId());
            return Ok(room);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = roomService.Delete(id);
            return Ok(room);
        }
    }
}
