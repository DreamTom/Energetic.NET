using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IRepositories;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class ResourceDomainRepository(BasicDbContext basicDbContext) :
        BaseRepository<Resource>(basicDbContext), IResourceDomainRepository
    {
        public Task<Resource?> FindByApiUrlAndRequestMethodAsync(string apiUrl, RequestMethod requestMethod)
        {
            return GetQueryableSet().SingleOrDefaultAsync(r => r.ApiUrl == apiUrl && r.RequestMethod == requestMethod && r.IsEnable);
        }

        public async Task<List<Resource>> GetAllEnableResourcesAsync(bool ingoreButton = false)
        {
            var query = GetQueryableSet().Where(r => r.IsEnable);
            return ingoreButton ? await query.Where(r => r.Type != ResourceType.Button).ToListAsync()
                : await query.ToListAsync();
        }

        public Task<List<Resource>> GetResourcesAsync(string? name, string? routePath, string? code)
        {
            var query = basicDbContext.Resources.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(r => r.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(routePath))
                query = query.Where(r => r.RoutePath != null && r.RoutePath.Contains(routePath));
            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(r => r.Code != null && r.Code.Contains(code));
            return query.OrderBy(r => r.DisplayOrder).ToListAsync();
        }

        public Task<bool> IsExistsApiAsync(string apiUrl, RequestMethod requestMethod, long id = 0)
        {
            if (id > 0)
                return basicDbContext.Resources.AnyAsync(r => r.ApiUrl == apiUrl && r.RequestMethod == requestMethod
                       && r.Type == ResourceType.Button && r.Id != id);
            return basicDbContext.Resources.AnyAsync(r => r.ApiUrl == apiUrl && r.RequestMethod == requestMethod
                   && r.Type == ResourceType.Button);
        }

        public Task<bool> IsExistsCodeAsync(string code, long id = 0)
        {
            if (id > 0)
                return basicDbContext.Resources.AnyAsync(r => r.Code == code && r.Type == ResourceType.Button && r.Id != id);
            return basicDbContext.Resources.AnyAsync(r => r.Code == code && r.Type == ResourceType.Button);
        }

        public Task<bool> IsExistsPathAsync(string routePath, long id = 0)
        {
            if (id > 0)
                return basicDbContext.Resources.AnyAsync(r => r.RoutePath == routePath && r.Type == ResourceType.Menu && r.Id != id);
            return basicDbContext.Resources.AnyAsync(r => r.RoutePath == routePath && r.Type == ResourceType.Menu);
        }
    }
}
