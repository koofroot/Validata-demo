using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;
using Validata.Infrastructure.Exceptions;
using Validata.Infrastructure.Models.Dtos;

namespace Validata.Infrastructure.Handlers.CustomerHandlers
{
    public record GetCustomerCommand(Guid Id) : IRequest<CustomerDto>;
    internal class GetCustomerRequestHandler : IRequestHandler<GetCustomerCommand, CustomerDto>
    {
        private readonly IReadonlyRepository<Customer> _customerRepository;

        public GetCustomerRequestHandler(IReadonlyRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = await _customerRepository.GetAsync(request.Id, x => x.Address);

            if (cust == null)
            {
                throw new CustomerNotFoundException();
            }
            var dto = new CustomerDto
            {
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                Id = cust.Id,
                Address = new AddressDto
                {
                    StreetAddress = cust.Address.StreetAddress,
                    PostalCode = cust.Address.PostalCode
                }
            };

            return dto;
        }
    }
}
