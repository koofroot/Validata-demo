using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;

namespace Validata.Infrastructure.Handlers.ProductHandlers
{
    public record CreateProductCommand(string Name, decimal Price) : IRequest;
    internal class CreateProductRequestHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IRepository<Product> _repository;

        public CreateProductRequestHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            await _repository.CreateAsync(product);

            await _repository.SaveChangesAsync();
        }
    }
}
