using Energetic.NET.Basic.Application.Resource.Dto;

namespace Energetic.NET.Basic.Application.Resource
{
    public interface IResourceAppService
    {
        Task<List<ResourceTreeResponse>> GetResourceTreeAsync(ResourceQueryRequest resourceQuery);

        Task<List<TreeResponse>> GetMenuTreeAsync();
    }
}
