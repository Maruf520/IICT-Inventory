using IICT_Store.Models;

namespace IICT_Store.Dtos.Rooms
{
    public class CreateRoomDto
    {
        public int RoomNo { get; set; }
        public RoomType RoomType { get; set; }
    }
}