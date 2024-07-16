using MediatR;
using Validata.Data.Interfaces;
using Validata.Data.Models;

namespace Validata.Infrastructure
{
    public abstract class RequestHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> repositiry;

        protected RequestHandler(IRepository<TEntity> repositiry)
        {
            this.repositiry = repositiry;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
