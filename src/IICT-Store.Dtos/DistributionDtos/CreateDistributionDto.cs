using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.DistributionDtos
{
    public class CreateDistributionDto
    {
        public int RoomNo { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string NameOfUser { get; set; }
        public string Description { get; set; }
        [Required]
        public int OrderNo { get; set; }
        [Required]
        public long SenderId { get; set; } //rakesh account reference
        [Required]
        public long ReceiverId { get; set; }
        public ICollection<ProductSerialNoDto> ProductSerialNo { get; set; }
        public int DistributedTo { get; set; } // to samir sir/ room no 212
        public string SignatureOfReceiver { get; set; }
        public string Note { get; set; }
    }
}
