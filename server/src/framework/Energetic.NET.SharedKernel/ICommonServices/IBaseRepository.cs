using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.SharedKernel.ICommonServices
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity, IAggregateRoot
    {
        ValueTask<TEntity?> FindByIdAsync(long id);

        Task<bool> IsExistsIdAsync(long id);

        IQueryable<TEntity> GetQueryableSet();

        Task<TEntity> AddAsync(TEntity entity);

        void Update(TEntity entity);

        void LogicDelete(TEntity enity);

        void Delete(TEntity entity);
    }
}
