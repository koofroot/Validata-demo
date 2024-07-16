using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;

namespace Validata.Infrastructure.Handlers.CustomerHandlers
{
    public record CreateCustomerCommand(string FirstName, string LastName, string Address, string PostalCode) : IRequest<Guid>;
    internal class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly IRepository<Customer> _customerRepository;

        public CreateCustomerRequestHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = new Address
                {
                    StreetAddress = request.Address,
                    PostalCode = request.PostalCode,
                }
            };

            var custId = await _customerRepository.CreateAsync(cust);

            await _customerRepository.SaveChangesAsync();

            return custId;
        }
    }
}
