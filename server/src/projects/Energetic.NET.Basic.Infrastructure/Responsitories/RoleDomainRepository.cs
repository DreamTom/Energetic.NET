using Energetic.NET.Basic.Domain.IRepositories;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class RoleDomainRepository(BasicDbContext basicDbContext)
        : BaseRepository<Role>(basicDbContext), IRoleDomainRepository
    {
        public async Task<Role> GetRoleIncludeResourcesAsync(long roleId)
        {
            return await GetQueryableSet()
                    .Include(c => c.Resources.OrderBy(r => r.DisplayOrder))
                    .SingleAsync(c => c.Id == roleId);
        }

        public Task<bool> IsExistsCodeAsync(string code, long id = 0)
        {
            if (id > 0)
                return GetQueryableSet().AnyAsync(x => x.Code == code && x.Id != id);
            return GetQueryableSet().AnyAsync(x => x.Code == code);
        }
    }
}
