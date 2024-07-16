using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Validata.Data.Interfaces;
using Validata.Data.Models;

namespace Validata.Data.Repositories
{
    public class ReadonlyRepository<TEntity> : IReadonlyRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ValidataContext _context;
        private readonly DbSet<TEntity> _set;

        public ReadonlyRepository(ValidataContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public Task<TEntity?> GetAsync(Guid id, Expression<Func<TEntity, BaseEntity>> include)
        {
            return Task.FromResult(_set
                .Include(include)
                .Where(NotDeleted)
                .FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate, Expression<Func<TEntity, BaseEntity>> include)
        {
            return Task.FromResult(_set
                .Include(include)
                .Where(NotDeleted)
                .Where(predicate));
        }

        public Task<TEntity?> GetAsync(Guid id)
        {
            return Task.FromResult(_set
                .Where(NotDeleted)
                .FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(
                _set.Where(NotDeleted).Where(predicate));
        }

        private static bool NotDeleted(TEntity entyty)
        {
            return !entyty.IsDeleted;
        }

        public Task<IEnumerable<TEntity>> GetAsync<TCollection>(Func<TEntity, bool> predicate, Expression<Func<TEntity, ICollection<TCollection>>> include) 
            where TCollection : BaseEntity
        {
            return Task.FromResult(_set
                .Include(include)
                .Where(NotDeleted)
                .Where(predicate));
        }

        public Task<TEntity?> GetAsync<TCollection>(Guid id, Expression<Func<TEntity, ICollection<TCollection>>> include)
            where TCollection : BaseEntity
        {
            return Task.FromResult(_set
                .Include(include)
                .Where(NotDeleted)
                .FirstOrDefault(x => x.Id == id));
        }
    }
}
