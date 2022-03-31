using System.ComponentModel.DataAnnotations;

namespace BigShop.ViewModels.ProductsViewModels
{
    public class CreateProductVM
    {
        [Required,Display(Name ="Названия продукта")]
        public string? ProductName { get; set; }

        [Required,Display(Name ="Описания")]
        public string? ProductDescription { get; set; }

        [Required,Display(Name ="Цена")]
        public decimal? Price { get; set; }

        [Required,Display(Name ="Категория товара")]
        public Guid? CategoryId { get; set; }

        [Required,Display(Name ="Фото товара")]
        public IFormFile? ImgFile { get; set; }
    }
}
