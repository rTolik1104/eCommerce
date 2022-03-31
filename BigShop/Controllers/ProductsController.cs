using BigShop.Data;
using BigShop.Data.Interfaces;
using BigShop.ViewModels.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly AppDbContext _dbContext;

        public ProductsController(IProductServices productServices,AppDbContext dbContext)
        {
            _productServices = productServices;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            var products = await _productServices.GetAllProductsAsync();
            products = products.Where(x => x.Enabled == true);

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products
                    .Where(x => x.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        || x.ProductDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            var productsList = products.Select(x => new ProductVM
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductImgFileName = x.ImgPath,
                Price = x.ProductPrice
            });

            var model = new ProductListVM
            {
                Products = productsList
            };

            return View(model);
        }

        public async Task<IActionResult> ProductsByCategory(Guid categoryId)
        {
            var products = await _productServices.GetProductsByCategoryId(categoryId);
            products = products.Where(x => x.Enabled == true);

            var category = await _dbContext.Categories.Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).FirstOrDefaultAsync();
            ViewBag.Category = category;

            var productsList = products.Select(x => new ProductVM
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductImgFileName = x.ImgPath,
                Price = x.ProductPrice
            });

            var model = new ProductListVM
            {
                Products = productsList
            };

            return View(model);
        }

        public async Task<IActionResult> Details(Guid productId)
        {
            var product = await _productServices.GetProductByIdAsync(productId);
            var category = await _dbContext.Categories.Where(x => x.CategoryId == product.CategoryId).Select(x => x.CategoryName).FirstOrDefaultAsync();
            ViewBag.Category = category;
            var model = new ProductVM
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductImgFileName = product.ImgPath,
                ProductDescription = product.ProductDescription,
                Price = product.ProductPrice,
                Category = product.CategoryId
            };

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
