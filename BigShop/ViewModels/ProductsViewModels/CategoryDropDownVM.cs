using BigShop.Models;

namespace BigShop.ViewModels.ProductsViewModels
{
    public class CategoryDropDownVM
    {
        public CategoryDropDownVM()
        {
            Categories = new List<Categories>();
        }
        public List<Categories>? Categories { get; set; }
    }
}
