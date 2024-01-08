
namespace Energetic.NET.Basic.Domain.IRepositories
{
    public interface IRoleDomainRepository : IBaseRepository<Role>
    {
        Task<bool> IsExistsCodeAsync(string code, long id = 0);
    }
}
