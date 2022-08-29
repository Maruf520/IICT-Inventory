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
        public long? ReceiverId { get; set; }
        public Person Receiver { get; set; }
        public long? SenderId { get; set; }
        public Person Sender { get; set; }
        public int RoomNo { get; set; } //Damaged from room
        public long? PersonId { get; set; } //Damaged from single user
 //       public Person Person { get; set; }
        public DamagedFrom DamagedFrom { get; set; }
        public decimal Quantity { get; set; }
        public bool WasNotDistributed { get; set; }
        public ICollection<DamagedProductSerialNo> DamagedProductSerialNos { get; set; }
    }

    public enum DamagedFrom
    {
        Person,
        Room
    }
}
