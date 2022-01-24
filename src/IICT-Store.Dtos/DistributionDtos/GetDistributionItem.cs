using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.DistributionDtos
{
    public class GetDistributionItem
    {
        public int Total { get; set; }
        public int RoomNo { get; set; }
        public List<ProductSerialNoDto> productSerialNoDtos { get; set; }
    }
}
