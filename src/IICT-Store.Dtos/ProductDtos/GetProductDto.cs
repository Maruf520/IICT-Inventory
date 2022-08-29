using IICT_Store.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.ProductDtos
{
    public class GetProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal QuantityInStock { get; set; }
        public decimal TotalQuantity { get; set; }
        public long CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public GetUserDto CreatedByUser { get; set; }
        public GetUserDto UpdatedByUser { get; set; }
        public bool HasSerial { get; set; }
        public decimal NotSerializedProduct { get; set; }

    }
}
