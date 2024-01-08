using Energetic.NET.Basic.Domain.IRepositories;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class RoleDomainRepository(BasicDbContext basicDbContext)
        : BaseRepository<Role>(basicDbContext), IRoleDomainRepository
    {
        public Task<bool> IsExistsCodeAsync(string code, long id = 0)
        {
            if (id > 0)
                return GetQueryableSet().AnyAsync(x => x.Code == code && x.Id != id);
            return GetQueryableSet().AnyAsync(x => x.Code == code);
        }
    }
}
