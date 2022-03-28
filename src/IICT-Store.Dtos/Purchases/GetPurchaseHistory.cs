using IICT_Store.Dtos.ProductDtos;

namespace IICT_Store.Dtos.Purchases
{
    public class GetPurchaseHistory
    {
        public GetProductDto Product { get; set; }
        public int YearOfPurchase { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQuntity { get; set; }
        public int Year { get; set; }

    }
}