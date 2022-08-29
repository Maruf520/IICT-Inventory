using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.ProductDtos
{
    public class CreateDamagedProductDto
    {
        public long ProductId { get; set; }
        public long SerialId { get; set; }
        public int DistributionId { get; set; }
        public decimal Quantity { get; set; }
        [Required]
        public long SenderId { get; set; }
        [Required]
        public long ReceiverId { get; set; }

    }
}
