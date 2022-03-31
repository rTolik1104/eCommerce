using System.ComponentModel.DataAnnotations;

namespace BigShop.Models
{
    public class Products
    {
        [Key]
        public Guid ProductId { get; set; }= Guid.NewGuid();
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set;}
        public decimal? ProductPrice { get; set; }
        public string? ImgPath { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? Enabled { get; set; }
    }
}
