using System.Collections.Generic;
using IICT_Store.Dtos.Rooms;
using IICT_Store.Models;
using IICT_Store.Repositories;

namespace IICT_Store.Services.RoomServices
{
    public interface IRoomService
    {
        public ServiceResponse<GetRoomDto> CreateRoom(CreateRoomDto createRoomDto, string userId);
        public ServiceResponse<GetRoomDto> UpdateRoom(CreateRoomDto createRoomDto, int roomId, string userId);
        public ServiceResponse<GetRoomDto> GetById(int id);
        public ServiceResponse<List<GetRoomDto>> GetAllRoom();
        public ServiceResponse<GetRoomDto> Delete(int id);


    }
}