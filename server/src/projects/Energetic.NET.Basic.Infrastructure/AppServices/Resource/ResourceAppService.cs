using Energetic.NET.Basic.Application.Resource;
using Energetic.NET.Basic.Application.Resource.Dto;
using Energetic.NET.Basic.Domain.IResponsitories;
using MapsterMapper;

namespace Energetic.NET.Basic.Infrastructure.AppServices.Resource
{
    internal class ResourceAppService(IResourceDomainRepository resourceDomainRepository,
        IMapper mapper) : IResourceAppService
    {
        public async Task<List<ResourceTreeResponse>> GetResourceTreeAsync()
        {
            var nodeTree = new List<ResourceTreeResponse>();
            var resources = await resourceDomainRepository.GetResourcesAsync();
            var allNodes = mapper.Map<List<ResourceTreeResponse>>(resources);
            var parentNodes = allNodes.Where(r => r.ParentId == 0).ToList();
            foreach (var parentNode in parentNodes)
            {
                SetChildNodes(allNodes, parentNode);
                nodeTree.Add(parentNode);
            }
            return nodeTree;
        }

        private static void SetChildNodes(List<ResourceTreeResponse> allNodes, ResourceTreeResponse parentNode)
        {
            var childNodes = allNodes.Where(x => x.ParentId == parentNode.Id).ToList();
            parentNode.Children.AddRange(childNodes);
            foreach (var childNode in childNodes)
            {
                SetChildNodes(allNodes, childNode);
            }
        }
    }
}
