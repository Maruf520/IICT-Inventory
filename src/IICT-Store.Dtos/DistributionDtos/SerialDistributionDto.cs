using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.DistributionDtos
{
    public class SerialDistributionDto
    {
        public int RoomNo { get; set; }
        public ICollection<ProductSerialNoDto> ProductSerialNoDtos { get; set; }
    }
}
