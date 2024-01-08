using Energetic.NET.Basic.Application.ResourceService.Dto;

namespace Energetic.NET.Basic.Application.ResourceService
{
    public interface IResourceAppService
    {
        Task<List<ResourceTreeResponse>> GetResourceTreeAsync(ResourceQueryRequest resourceQuery);

        Task<List<TreeResponse>> GetMenuTreeAsync();
    }
}
