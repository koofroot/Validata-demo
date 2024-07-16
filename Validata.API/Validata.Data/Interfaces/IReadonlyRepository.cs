using System.Linq.Expressions;
using Validata.Data.Models;

namespace Validata.Data.Interfaces
{
    public interface IReadonlyRepository<TEntity> where TEntity : BaseEntity
    {

        Task<TEntity?> GetAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate);

        Task<TEntity?> GetAsync(Guid id, Expression<Func<TEntity, BaseEntity>> include);

        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate, Expression<Func<TEntity, BaseEntity>> include);

        Task<IEnumerable<TEntity>> GetAsync<TCollection>(Func<TEntity, bool> predicate, Expression<Func<TEntity, ICollection<TCollection>>> include) 
            where TCollection: BaseEntity;

        Task<TEntity?> GetAsync<TCollection>(Guid id, Expression<Func<TEntity, ICollection<TCollection>>> include) 
            where TCollection : BaseEntity;
    }
}
