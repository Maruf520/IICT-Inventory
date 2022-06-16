using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.DistributionDtos
{
    public class GetDistributionDto
    {
        public long Id { get; set; }
        public int RoomNo { get; set; }
        public long ProductId { get; set; }
        public GetProductDto Product { get; set; }
        public int Quantity { get; set; }
        public int TotalRemainingQuantity { get; set; }
        public string NameOfUser { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public long SenderId { get; set; } //rakesh account reference
        public long ReceiverId { get; set; }
        public ICollection<ProductSerialNoDto> ProductSerialNo { get; set; }
        public ICollection<GetProductSerialDto> GetProductSerialNo { get; set; }
        public int DistributedTo { get; set; } // to samir sir/ room no 212
        public string SignatureOfReceiver { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public GetUserDto UpdatedByUser { get; set; }
        public GetUserDto CreatedByByUser { get; set; }
        public string UpdatedBy { get; set; }
    }
}
