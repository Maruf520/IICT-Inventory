using System.Collections.Generic;
using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models.Products;

namespace IICT_Store.Dtos.ProductDtos
{
    public class GetMaintananceProductDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public GetProductDto Product { get; set; }
        public GetUserDto CreatedByUser { get; set; }
        public long DistributionId { get; set; }
        public long ReceiverId { get; set; }
        public long SenderId { get; set; }
        public decimal Quantity { get; set; }
        public string Note { get; set; }
        public List<MaintenanceProductSerialNo> MaintananceProductSerial { get; set; }
    }
}