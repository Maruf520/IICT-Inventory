using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.ProductDtos
{
    public class GetDamagedProductDto
    {
        public int Quantity { get; set; }
        public List<DamagedProductDto> DamagedProducts { get; set; }
    }
}
