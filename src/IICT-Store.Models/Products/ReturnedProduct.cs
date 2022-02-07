using IICT_Store.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class ReturnedProduct : BaseModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int ReceiverId { get; set; }
        public Person Receiver { get; set; }
        public int SenderId { get; set; }
        public Person Sender { get; set; }
        public ICollection<ReturnedProductSerialNo> ReturnedProductSerialNos { get; set; }
        public string Note { get; set; }
    }
}
