using Energetic.NET.SharedKernel;
using Energetic.NET.SharedKernel.ICommonServices;
using Energetic.NET.SharedKernel.IModels;
using Microsoft.EntityFrameworkCore;

namespace Energetic.NET.Infrastructure.EFCore
{
    public class BaseRepository<TEntity>(BaseDbContext baseDbContext) : IBaseRepository<TEntity>
        where TEntity : class, IEntity, IAggregateRoot
    {
        protected readonly DbSet<TEntity> _dbSet = baseDbContext.Set<TEntity>();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await baseDbContext.AddAsync(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<bool> IsExistsIdAsync(long id)
        {
            return _dbSet.AnyAsync(x => x.Id == id);
        }

        public ValueTask<TEntity?> FindByIdAsync(long id)
        {
            return _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetQueryableSet()
        {
            return _dbSet;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public Task<List<TEntity>> FindByIdsAsync(IEnumerable<long> ids)
        {
            return _dbSet.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }
    }
}
