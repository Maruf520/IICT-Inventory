using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class DamagedProductSerialNo : BaseModel
    {
        public string Name { get; set; }
        public long ProductNoId { get; set; }
        public long DamagedProductId { get; set; }
        public DamagedProduct DamagedProduct { get; set; }
    }
}
