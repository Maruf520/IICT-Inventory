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
        public int Quantity { get; set; }
        public ICollection<DamagedProductSerialNo> DamagedProductSerialNos { get; set; }
    }
}
