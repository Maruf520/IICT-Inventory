using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.ProductDtos
{
    public class GetDamagedProductDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long ReceiverId { get; set; }
        public long SenderId { get; set; }
        public int RoomNo { get; set; } //Damaged from room
        public long PersonId { get; set; } //Damaged from single user
        public DamagedFrom DamagedFrom { get; set; }
        public int Quantity { get; set; }
        public bool WasNotDistributed { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public enum DamagedFrom
    {
        Person,
        Room
    }
}
