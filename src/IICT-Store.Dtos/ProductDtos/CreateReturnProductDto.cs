using IICT_Store.Models.Persons;
using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.ProductDtos
{
    public class CreateReturnProductDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public List<int> ReturnedProductSerialNos { get; set; }
        public string Note { get; set; }
    }
}
