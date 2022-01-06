using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class ProductSerialNo : BaseModel
    {
        public string ProductNoId { get; set; }
        public ProductNo ProductNo { get; set; }
        public long DistributionId { get; set; }
        public Distribution Distribution { get; set; }
    }
}
