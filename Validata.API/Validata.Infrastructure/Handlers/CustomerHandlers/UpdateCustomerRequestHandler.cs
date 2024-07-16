using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;
using Validata.Infrastructure.Exceptions;

namespace Validata.Infrastructure.Handlers.CustomerHandlers
{
    public record UpdateCustomerCommand(Guid Id, string FirstName, string LastName) : IRequest;
    internal class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IReadonlyRepository<Customer> _readonlyRepository;

        public UpdateCustomerRequestHandler(IRepository<Customer> repository, IReadonlyRepository<Customer> readonlyRepository)
        {
            _repository = repository;
            _readonlyRepository = readonlyRepository;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = await _readonlyRepository.GetAsync(request.Id);

            if (cust == null)
            {
                throw new CustomerNotFoundException();
            }

            cust.FirstName = request.FirstName;
            cust.LastName = request.LastName;

            await _repository.UpdateAsync(cust);

            await _repository.SaveChangesAsync();
        }
    }
}
