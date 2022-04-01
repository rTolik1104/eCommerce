using BigShop.Data.Interfaces;
using BigShop.Models;
using BigShop.ViewModels.ProductsViewModels;
using Microsoft.EntityFrameworkCore;

namespace BigShop.Data.Services
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHost;
        private readonly ILogger _logger;

        public ProductServices(AppDbContext appDbContext, ILogger<ProductServices> logger, IWebHostEnvironment webHost)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _webHost = webHost;
        }

        public async Task AddAsync(Products product)
        {
            try
            {
                if (product != null)
                {
                    await _appDbContext.Products.AddAsync(product);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid productId)
        {
            try
            {
                if (productId != null)
                {
                    var product = await GetProductByIdAsync(productId);
                    DeleteProductImg(product.ImgPath);
                    _appDbContext.Products.Remove(product);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            try
            {
                var products = await _appDbContext.Products.ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CategoryDropDownVM> GetCategoriesAsync()
        {
            var response = new CategoryDropDownVM()
            {
                Categories = await _appDbContext.Categories.ToListAsync()
            };
            return response;
        }

        public async Task<Products> GetProductByIdAsync(Guid? productId)
        {
            try
            {
                var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Products>> GetProductsByCategoryId(Guid categoryId)
        {
            try
            {
                var products = await GetAllProductsAsync();
                return products.Where(x => x.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task IsEnabled(Guid productId)
        {
            try
            {
                var product = await GetProductByIdAsync(productId);
                if (product.Enabled == false)
                {
                    product.Enabled = true;
                }
                else
                {
                    product.Enabled = false;
                }
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public string SaveImg(IFormFile formFile)
        {
            string uniqName = string.Empty;
            if (formFile.Name != null)
            {
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "productImg");
                uniqName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fs);
                    fs.Close();
                }
            }
            return uniqName;
        }
        public void DeleteProductImg(string ImgFileName)
        {
            string uploadFolder = Path.Combine(_webHost.WebRootPath, "productImg");
            string filePath = Path.Combine(uploadFolder, ImgFileName);
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }


        public async Task UpdateAsync(Products newProduct)
        {
            try
            {
                _appDbContext.Update(newProduct);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
