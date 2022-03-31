using BigShop.Data.Interfaces;
using BigShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BigShop.Data.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public OrderServices(AppDbContext appDbContext, ILogger<OrderServices> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }


        public async Task AddOrderAsync(Orders order)
        {
            try
            {
                await _appDbContext.Orders.AddAsync(order);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task DeleteAllOrders()
        {
            try
            {
                var orders = await GetAllOrdersAsync();
                foreach (var order in orders)
                {
                    _appDbContext.Orders.Remove(order);
                }
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            try
            {
                var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
                if (order != null)
                {
                    _appDbContext.Orders.Remove(order);
                }
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IEnumerable<Orders>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _appDbContext.Orders.ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Orders>();
            }
        }

        public async Task<Orders> GetOrderByIdAsync(Guid orderId)
        {
            try
            {
                var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task IsComplete(Guid orderId)
        {
            try
            {
                var order = await GetOrderByIdAsync(orderId);
                if (order != null)
                {
                    order.IsComplete = true;
                }
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
