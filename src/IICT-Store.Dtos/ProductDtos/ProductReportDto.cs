namespace IICT_Store.Dtos.ProductDtos
{
    public class ProductReportDto
    {
        public GetProductDto Product { get; set; }
        public long ProductId { get; set; }
        public decimal TotalProduct { get; set; }
        public decimal TotalDistributedProduct { get; set; }
        public decimal TotalDamagedProduct { get; set; }
        public decimal ProductInStock { get; set; }
        public decimal TotalMaintenanceProduct { get; set; }
        public decimal TotalBoughtProduct { get; set; }
    }
}