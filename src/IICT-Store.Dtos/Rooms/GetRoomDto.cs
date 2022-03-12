using IICT_Store.Models;

namespace IICT_Store.Dtos.Rooms
{
    public class GetRoomDto
    {
        public int Id { get; set; }
        public int RoomNo { get; set; }
        public RoomType RoomType { get; set; }
    }
}