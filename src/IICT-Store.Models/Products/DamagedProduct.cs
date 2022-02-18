using IICT_Store.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class DamagedProduct : BaseModel
    {
        public DamagedProduct()
        {
            DamagedProductSerialNos = new List<DamagedProductSerialNo>();
        }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int ReceiverId { get; set; }
        public Person Receiver { get; set; }
        public int SenderId { get; set; }
        public Person Sender { get; set; }
        public int RoomNo { get; set; } //Damaged from room
        public int PersonId { get; set; } //Damaged from single user
        public Person Person { get; set; }
        public DamagedFrom DamagedFrom { get; set; }
        public int Quantity { get; set; }
        public bool WasNotDistributed { get; set; }
        public ICollection<DamagedProductSerialNo> DamagedProductSerialNos { get; set; }
    }

    public enum DamagedFrom
    {
        Person,
        Room
    }
}
