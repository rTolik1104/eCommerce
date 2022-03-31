using BigShop.Models;

namespace BigShop.Data.Interfaces
{
    public interface IOrderServices
    {
        public Task<IEnumerable<Orders>> GetAllOrdersAsync();
        public Task<Orders> GetOrderByIdAsync(Guid orderId);

        public Task AddOrderAsync(Orders order);
        public Task DeleteOrderAsync(Guid orderId);
        public Task DeleteAllOrders();
        public Task IsComplete(Guid orderId);
    }
}
