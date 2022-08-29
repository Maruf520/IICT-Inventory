namespace IICT_Store.Dtos.ProductDtos
{
    public class CreateMaintananceProductDto
    {
        public long ProductId { get; set; }
        public long ProductSerialId { get; set; }
        public decimal Quantity { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string Note { get; set; }
    }
}