using Validata.Data.Interfaces;
using Validata.Data.Models;

namespace Validata.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ValidataContext _context;

        public Repository(ValidataContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(TEntity entity)
        {
            var res = await _context.AddAsync(entity);

            return res.Entity.Id;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);

            return Task.CompletedTask;
        }
    }
}
