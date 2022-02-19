using IICT_Store.Models.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class Distribution : BaseModel
    {
/*        public Distribution()
        {
            ProductSerialNo = new List<ProductSerialNo>();
        }*/
        public int? RoomNo { get; set; }
        public long  ProductId { get; set; }
        public Product  Product { get; set; }
        public int Quantity { get; set; }
        public string NameOfUser { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public int TotalRemainingQuantity { get; set; }

        //foreignKey
        [ForeignKey("SenderId")]
        public long SenderId { get; set; } //rakesh account reference
        public Person Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public long? ReceiverId { get; set; }
        public Person Receiver { get; set; }
/*        public ICollection<ProductSerialNo> ProductSerialNo { get; set; }*/
        public long DistributedTo { get; set; } // to samir sir/ room no 212
        public string SignatureOfReceiver { get; set; }
        public string Note { get; set; }
    }
}
