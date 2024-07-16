using Validata.Data.Models;

namespace Validata.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByCustomerId(Guid clientId);

        Task<Order?> GetOrderByOrderId(Guid orderId);

        Task<IEnumerable<Order>> GetOpenOrders();
    }
}
