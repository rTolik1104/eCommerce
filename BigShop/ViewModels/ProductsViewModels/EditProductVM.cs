namespace BigShop.ViewModels.ProductsViewModels
{
    public class EditProductVM
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? Price { get; set; }
        public string? ImgFileName { get; set; }
        public IFormFile? ImgFile { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Enabled { get; set; }
    }
}
