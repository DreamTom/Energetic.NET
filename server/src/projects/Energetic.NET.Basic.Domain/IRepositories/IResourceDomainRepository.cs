using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Domain.IRepositories
{
    public interface IResourceDomainRepository : IBaseRepository<Resource>
    {
        Task<bool> IsExistsPathAsync(string routePath, long id = 0);

        Task<bool> IsExistsCodeAsync(string code, long id = 0);

        Task<bool> IsExistsApiAsync(string apiUrl, RequestMethod requestMethod, long id = 0);

        Task<List<Resource>> GetResourcesAsync(string? name, string? routePath, string? code);

        Task<List<Resource>> GetResourcesIgnoreButtonsAsync();
    }
}
