using BigShop.ViewModels.OrdersViewModel;
using BigShop.ViewModels.ProductsViewModels;

namespace BigShop.ViewModels.AdminViewModels
{
    public class IndexVM
    {
        public IEnumerable<OrderVM>? Orders { get; set; }
        public int? ProductsCount { get; set; }
        public int? OrdersCount { get; set; }
        public int? CategoriesCount { get; set; }
    }
}
