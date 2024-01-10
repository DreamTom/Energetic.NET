using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.SharedKernel.ICommonServices
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity, IAggregateRoot
    {
        ValueTask<TEntity?> FindByIdAsync(long id);

        Task<List<TEntity>> FindByIdsAsync(IEnumerable<long> ids);

        Task<List<TEntity>> GetAllAsync();

        Task<bool> IsExistsIdAsync(long id);

        IQueryable<TEntity> GetQueryableSet();

        Task<TEntity> AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
