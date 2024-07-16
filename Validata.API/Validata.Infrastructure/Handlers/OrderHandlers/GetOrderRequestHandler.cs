using MediatR;
using Validata.Data.Interfaces;
using Validata.Infrastructure.Exceptions;
using Validata.Infrastructure.Models.Dtos;

namespace Validata.Infrastructure.Handlers.OrderHandlers
{
    public record GetOrderCommand(Guid CustomerId) : IRequest<OrderDto>;
    internal class GetOrderRequestHandler : IRequestHandler<GetOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _repository;

        public GetOrderRequestHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderDto> Handle(GetOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetOrderByCustomerId(request.CustomerId);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            var dto = new OrderDto
            {
                OrderDate = order.OrderDate,
                OrderId = request.CustomerId,
                TotalPrice = order.TotalPrice,
                IsCompleted = order.IsCompleted,
                Items = order.Items.Select(x => new ItemDto
                {
                    Count = x.Count,
                    Product = new ProductDto
                    {
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                    }
                })
            };

            return dto;
        }
    }
}
