using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;
using Validata.Infrastructure.Exceptions;

namespace Validata.Infrastructure.Handlers.OrderHandlers
{
    public record CloseOrederCommand(Guid Id): IRequest;
    internal class CloseOrederRequestHandler : IRequestHandler<CloseOrederCommand>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IReadonlyRepository<Order> _orderRepositoryReadonly;

        public CloseOrederRequestHandler(IRepository<Order> orderRepository, IReadonlyRepository<Order> orderRepositoryReadonly)
        {
            _orderRepository = orderRepository;
            _orderRepositoryReadonly = orderRepositoryReadonly;
        }

        public async Task Handle(CloseOrederCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepositoryReadonly.GetAsync(request.Id);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            order.IsCompleted = true;

            await _orderRepository.UpdateAsync(order);

            await _orderRepository.SaveChangesAsync();
        }
    }
}
