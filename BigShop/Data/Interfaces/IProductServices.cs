using BigShop.Models;
using BigShop.ViewModels.ProductsViewModels;

namespace BigShop.Data.Interfaces
{
    public interface IProductServices
    {
        public Task<Products> GetProductByIdAsync (Guid? productId);
        public Task<IEnumerable<Products>> GetAllProductsAsync ();
        public Task<IEnumerable<Products>> GetProductsByCategoryId (Guid categoryId);
        public Task<CategoryDropDownVM> GetCategoriesAsync();

        public Task AddAsync(Products product);
        public Task UpdateAsync(Products product);
        public Task DeleteAsync(Guid productId);
        public Task IsEnabled(Guid productId);
        public string SaveImg(IFormFile formFile);
        public void DeleteProductImg(string FileName);
    }
}
