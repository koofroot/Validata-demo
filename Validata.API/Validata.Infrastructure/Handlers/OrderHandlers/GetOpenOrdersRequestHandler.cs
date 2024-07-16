using MediatR;
using Validata.Data.Interfaces;
using Validata.Infrastructure.Models.Dtos;

namespace Validata.Infrastructure.Handlers.OrderHandlers
{
    public record GetOpenOrdersCommand() : IRequest<IEnumerable<OrderDto>>;
    internal class GetOpenOrdersRequestHandler : IRequestHandler<GetOpenOrdersCommand, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOpenOrdersRequestHandler(IOrderRepository orderReadonlyRepository)
        {
            _orderRepository = orderReadonlyRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOpenOrdersCommand request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOpenOrders();

            var result = orders.Select(x => new OrderDto
            {
                OrderDate = x.OrderDate,
                OrderId = x.Id,
                TotalPrice = x.TotalPrice,
                Items = x.Items.Select(i => new ItemDto
                {
                    Count = i.Count,
                    Product = new ProductDto
                    {
                        Name = i.Product.Name,
                        Price = i.Product.Price,
                    }
                })
            });

            return result;
        }
    }
}
