using System;
using System.Text.Json.Serialization;

namespace IICT_Store.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNo { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
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