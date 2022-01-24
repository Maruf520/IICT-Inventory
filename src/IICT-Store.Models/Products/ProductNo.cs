using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class ProductNo : BaseModel
    {
        public string Name { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
