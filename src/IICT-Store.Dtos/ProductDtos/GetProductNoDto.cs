using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.ProductDtos
{
    public class GetProductNoDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public int RoomNo { get; set; }
        public long DistributedTo { get; set; }
        public GetUserDto CreatedByUser { get; set; }
        public GetUserDto UpdatedByUser { get; set; }
    }
}
