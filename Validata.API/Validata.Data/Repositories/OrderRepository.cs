using Microsoft.EntityFrameworkCore;
using Validata.Data.Interfaces;
using Validata.Data.Models;

namespace Validata.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ValidataContext _context;
        private readonly DbSet<Order> _orders;

        public OrderRepository(ValidataContext context)
        {
            _context = context;
            _orders = context.Set<Order>();
        }

        public Task<IEnumerable<Order>> GetOpenOrders()
        {
            return Task.FromResult(
                _orders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => !x.IsDeleted && !x.IsCompleted)
                .AsEnumerable());
        }

        public Task<Order?> GetOrderByCustomerId(Guid customerId)
        {
            return Task.FromResult(
                _orders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => !x.IsDeleted && x.CustomerId == customerId)
                .OrderByDescending(x => x.OrderDate)
                .FirstOrDefault());
        }

        public Task<Order?> GetOrderByOrderId(Guid orderId)
        {
            return Task.FromResult(
               _orders
               .Include(x => x.Customer)
               .Include(x => x.Items)
               .ThenInclude(x => x.Product)
               .SingleOrDefault(x => !x.IsDeleted && x.Id == orderId));
        }
    }
}
