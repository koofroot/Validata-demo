using Validata.Data.Models;

namespace Validata.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<Guid> CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
