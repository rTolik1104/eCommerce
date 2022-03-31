using BigShop.Data;
using BigShop.Data.Interfaces;
using BigShop.Models;
using BigShop.ViewModels.AdminViewModels;
using BigShop.ViewModels.OrdersViewModel;
using BigShop.ViewModels.ProductsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BigShop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly IOrderServices _orderServices;
        private readonly AppDbContext _appDbContext;

        public AdminController(IProductServices productServices, IOrderServices orderServices,AppDbContext appDbContext)
        {
            _orderServices = orderServices;
            _productServices = productServices;
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productServices.GetAllProductsAsync();
            var orders = await _orderServices.GetAllOrdersAsync();
            var categories=await _productServices.GetCategoriesAsync();
            var orderList = orders.Select(x => new OrderVM
            {
                UserName = x.UserName,
                Phone = x.Phone,
                Region = x.Region,
                CreatedDate = x.Created,
                IsComplete = x.IsComplete,
            });

            var model = new IndexVM
            {
                Orders = orderList.OrderByDescending(x=>x.OrderId).Take(10),
                ProductsCount = products.Count(),
                OrdersCount = orders.Count(),
                CategoriesCount=categories.Categories.Count()
            };
            return View(model);
        }

        public async Task<IActionResult> AllProducts()
        {
            var products = await _productServices.GetAllProductsAsync();
            var categories = await _productServices.GetCategoriesAsync();

            var productList = products.Select(x => new ProductVM
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductDescription = x.ProductDescription,
                ProductImgFileName = x.ImgPath,
                Price = x.ProductPrice,
                ProductCategory = categories.Categories.Where(c=>c.CategoryId==x.CategoryId).First().CategoryName,
                Enabled = x.Enabled,
            });

            var model = new ProductListVM
            {
                Products = productList
            };

            return View(model);
        }

        public async Task<IActionResult> AllOrders()
        {
            var orders = await _orderServices.GetAllOrdersAsync();

            var orderList = orders.Select(x => new OrderVM
            {
                OrderId=x.OrderId,
                UserName = x.UserName,
                Phone = x.Phone,
                Region = x.Region,
                CreatedDate = x.Created,
                ProductName = x.ProductName,
                IsComplete = x.IsComplete,
            });

            var model = new OrderListVM
            {
                Orders = orderList
            };

            return View(model);
        }

        public async Task<IActionResult> Complete(Guid orderId)
        {
            var order = await _orderServices.GetOrderByIdAsync(orderId);

            if (order != null)
            {
                await _orderServices.IsComplete(orderId);
            }
            return RedirectToAction(nameof(AllOrders));
        }

        public async Task<IActionResult> AddProduct()
        {
            var categories = await _productServices.GetCategoriesAsync();

            ViewBag.Categories = new SelectList(categories.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductVM model)
        {
            if (ModelState.IsValid)
            {
                var product = new Products
                {
                    ProductDescription = model.ProductDescription,
                    ProductName = model.ProductName,
                    ProductPrice = model.Price,
                    CategoryId = model.CategoryId,
                    ImgPath = _productServices.SaveImg(model.ImgFile),
                    Enabled=true
                };

                await _productServices.AddAsync(product);
                return RedirectToAction(nameof(AllProducts));
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Inavlid create product");
            }
            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> EditProduct(Guid productId)
        {
            var product = await _productServices.GetProductByIdAsync(productId);
            var categories = await _productServices.GetCategoriesAsync();

            ViewBag.Categories = new SelectList(categories.Categories, "CategoryId", "CategoryName");

            if (product != null)
            {
                var model = new EditProductVM
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = product.ProductPrice,
                    ImgFileName = product.ImgPath,
                    CategoryId = product.CategoryId,
                    Enabled = product.Enabled.ToString()
                };

                return View(model);
            }
            return RedirectToAction(nameof(Products));
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductVM model)
        {
            if (ModelState.IsValid)
            {
                var newProduct = BuilEditProduct(model);
                await _productServices.UpdateAsync(newProduct);

                return RedirectToAction(nameof(AllProducts));
            }
            return View(model);
        }

        private Products BuilEditProduct(EditProductVM model)
        {
            var product = new Products
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                ProductPrice = model.Price,
                CategoryId = model.CategoryId
            };
            if (model.ImgFile != null)
            {
                product.ImgPath = _productServices.SaveImg(model.ImgFile);
            }
            else
            {
                product.ImgPath = model.ImgFileName;
            }
            if (model.Enabled == "Да")
            {
                product.Enabled = true;
            }
            else
            {
                product.Enabled = false;
            }
            return product;
        }

        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            if (productId != null)
            {
                await _productServices.DeleteAsync(productId);
            }
            return RedirectToAction(nameof(AllProducts));
        }

        public async Task<IActionResult> OrderDetails(Guid orderId)
        {
            var order = await _orderServices.GetOrderByIdAsync(orderId);

            var model = new OrderVM
            {
                OrderId = orderId,
                UserName = order.UserName,
                Phone = order.Phone,
                Address = order.Address,
                ProductName = order.ProductName,
                ProductCount = order.Count,
                Region = order.Region,
            };
            return View(model);
        }

        public async Task<IActionResult> DeleteOrders()
        {
            await _orderServices.DeleteAllOrders();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AllCategories()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            return View(categories);
        }
    }
}
