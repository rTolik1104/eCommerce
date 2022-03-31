using BigShop.Data.Interfaces;
using BigShop.Models;
using BigShop.ViewModels.OrdersViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BigShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IProductServices _productServices;

        public OrdersController(IOrderServices orderServices, IProductServices productServices)
        {
            _orderServices = orderServices;
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder(Guid productId)
        {
            var product = await _productServices.GetProductByIdAsync(productId);

            var model = new CreateOrderVM
            {
                ProductName = product.ProductName,
                ProductCount = 1
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderVM model)
        {
            if (ModelState.IsValid)
            {
                var order = new Orders
                {
                    UserName = model.UserName,
                    Phone = model.Phone,
                    Region = model.Region,
                    Address = model.Address,
                    ProductName = model.ProductName,
                    Count = model.ProductCount
                };

                await _orderServices.AddOrderAsync(order);
                return RedirectToAction(nameof(CompleteOrder));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid create order!");
            }
            return View(model);
        }

        public IActionResult CompleteOrder()
        {
            return View();
        }
    }
}
