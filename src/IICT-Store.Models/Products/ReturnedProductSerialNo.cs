using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class ReturnedProductSerialNo : BaseModel
    {
        public string Name { get; set; }
        public long ProductNoId { get; set; }
        public int ReturnedProductId { get; set; }
        public ReturnedProduct ReturnedProduct { get; set; }
    }
}
