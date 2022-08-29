using IICT_Store.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.ProductDtos
{
    public class DamagedProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string CreatedBy { get; set; }
        public GetUserDto CreatedByUser { get; set; }
        public string UpdatedBy { get; set; }
    }
}
