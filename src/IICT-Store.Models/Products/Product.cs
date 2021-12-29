using IICT_Store.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class Product : BaseModel
    {
        public Product()
        {
            Category = new Category();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int QuantityInStock { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
