using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;
using Validata.Infrastructure.Exceptions;

namespace Validata.Infrastructure.Handlers.OrderHandlers
{
    public record AddProductToOrderCommand(Guid CustomerId, Guid ProductId, int Count) : IRequest;
    internal class AddProductToOrderRequestHandler : IRequestHandler<AddProductToOrderCommand>
    {
        private readonly IReadonlyRepository<Customer> _customerReadonlyRepository;
        private readonly IReadonlyRepository<Product> _productReadonlyRepository;
        private readonly IOrderRepository _orderReadonlyRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<Order> _orderRepository;

        public AddProductToOrderRequestHandler(
            IReadonlyRepository<Customer> customerReadonlyRepository,
            IReadonlyRepository<Product> productReadonlyRepository,
            IOrderRepository orderReadonlyRepository,
            IRepository<Item> itemRepository,
            IRepository<Order> orderRepository)
        {
            _customerReadonlyRepository = customerReadonlyRepository;
            _productReadonlyRepository = productReadonlyRepository;
            _itemRepository = itemRepository;
            _orderReadonlyRepository = orderReadonlyRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(AddProductToOrderCommand request, CancellationToken cancellationToken)
        {
            var customerTask = _customerReadonlyRepository.GetAsync(request.CustomerId, x => x.Orders);
            var productTask = _productReadonlyRepository.GetAsync(request.ProductId);

            await Task.WhenAll(customerTask, productTask);
            var customer = customerTask.Result;
            var product = productTask.Result;

            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            if (customer == null)
            {
                throw new CustomerNotFoundException();
            }

            var currentOrder = await _orderReadonlyRepository.GetOrderByCustomerId(request.CustomerId);

            if (currentOrder == null || currentOrder.IsCompleted)
            {
                await CreateOrder(request, customer, product);

                await _orderRepository.SaveChangesAsync();

                return;
            }

            var orderItem = currentOrder.Items.SingleOrDefault(x => x.ProductId == request.ProductId);

            if (orderItem == null)
            {
                var item = new Item
                {
                    Count = request.Count,
                    Product = product,
                    Order = currentOrder,
                };
                currentOrder.Items.Add(item);
            }
            else
            {
                orderItem.Count += request.Count;
            }
            
            var totalPrice = currentOrder.Items
                .Where(x => !x.IsDeleted)
                .Select(x => x.Product.Price * x.Count)
                .Sum();

            currentOrder.TotalPrice = totalPrice < 0 ? 0 : totalPrice;

            await _orderRepository.UpdateAsync(currentOrder);

            await _orderRepository.SaveChangesAsync();
        }

        private async Task CreateOrder(AddProductToOrderCommand request, Customer customer, Product product)
        {
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                Customer = customer,
                TotalPrice = product.Price * request.Count
            };

            var item = new Item
            {
                Count = request.Count,
                Product = product,
                Order = order,
            };

            await _itemRepository.CreateAsync(item);
            await _orderRepository.CreateAsync(order);
        }
    }
}
