using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;
using Validata.Infrastructure.Exceptions;

namespace Validata.Infrastructure.Handlers.CustomerHandlers
{
    public record DeleteCustomerCommand(Guid Id) : IRequest;
    internal class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IReadonlyRepository<Customer> _readonlyRepository;

        public DeleteCustomerRequestHandler(IRepository<Customer> repository, IReadonlyRepository<Customer> readonlyRepository)
        {
            _repository = repository;
            _readonlyRepository = readonlyRepository;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = await _readonlyRepository.GetAsync(request.Id);

            if (cust == null)
            {
                throw new CustomerNotFoundException();
            }

            cust.IsDeleted = true;

            await _repository.UpdateAsync(cust);

            await _repository.SaveChangesAsync();
        }
    }
}
