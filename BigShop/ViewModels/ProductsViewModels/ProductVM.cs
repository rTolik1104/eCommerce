namespace BigShop.ViewModels.ProductsViewModels
{
    public class ProductVM
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? Price { get; set; }
        public string? ProductImgFileName { get; set; }
        public Guid? Category { get; set; }
        public string? ProductCategory { get; set; }
        public bool? Enabled { get; set; }
    }
}
