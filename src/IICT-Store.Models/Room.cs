namespace IICT_Store.Models
{
    public class Room:BaseModel
    {
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
    }

    public enum RoomType
    {
        DirectorOffice,
        IictOffice,
        ClassRoom,
        LabRoom,
        ConferrenceRoom,
        TeacherRoom,
        OfficerRoom,
        WashRoom,
        StoreRoom,
        Library
    }
}