namespace IICT_Store.Dtos.ProductDtos
{
    public class GetMaintananceProductSerialNoDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductNoId { get; set; }
        public bool IsRepaired { get; set; }
        public long MaintananceProductId { get; set; }
    }
}