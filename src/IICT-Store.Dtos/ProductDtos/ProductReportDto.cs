namespace IICT_Store.Dtos.ProductDtos
{
    public class ProductReportDto
    {
        public GetProductDto Product { get; set; }
        public long ProductId { get; set; }
        public int TotalProduct { get; set; }
        public int TotalDistributedProduct { get; set; }
        public int TotalDamagedProduct { get; set; }
        public int ProductInStock { get; set; }
        public int TotalMaintenanceProduct { get; set; }
        public int TotalBoughtProduct { get; set; }
    }
}