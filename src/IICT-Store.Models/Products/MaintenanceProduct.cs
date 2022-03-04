using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICT_Store.Models.Persons;

namespace IICT_Store.Models.Products
{
    public class MaintenanceProduct : BaseModel
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long ReceiverId { get; set; }
        public Person Receiver { get; set; }
        public long SenderId { get; set; }
        public Person Sender { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
