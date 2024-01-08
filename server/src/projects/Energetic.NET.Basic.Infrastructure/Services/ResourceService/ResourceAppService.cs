using Energetic.NET.Basic.Application.ResourceService;
using Energetic.NET.Basic.Application.ResourceService.Dto;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.SharedKernel;
using MapsterMapper;

namespace Energetic.NET.Basic.Infrastructure.ResourceService
{
    internal class ResourceAppService(IResourceDomainRepository resourceDomainRepository,
        IMapper mapper) : IResourceAppService
    {
        public async Task<List<ResourceTreeResponse>> GetResourceTreeAsync(ResourceQueryRequest resourceQuery)
        {
            var nodeTree = new List<ResourceTreeResponse>();
            var resources = await resourceDomainRepository.GetResourcesAsync(resourceQuery.Name, resourceQuery.RoutePath, resourceQuery.Code);
            var allNodes = mapper.Map<List<ResourceTreeResponse>>(resources);
            var parentNodes = allNodes.Where(r => r.ParentId == 0).ToList();
            if (parentNodes.Count == 0)
                return allNodes;
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
            if (childNodes.Count == 0)
                return;
            parentNode.Children = childNodes;
            foreach (var childNode in childNodes)
            {
                SetChildNodes(allNodes, childNode);
            }
        }

        public async Task<List<TreeResponse>> GetMenuTreeAsync()
        {
            var nodeTree = new List<TreeResponse>();
            var resources = await resourceDomainRepository.GetResourcesIgnoreButtonsAsync();
            var allNodes = mapper.Map<List<TreeResponse>>(resources);
            var parentNodes = allNodes.Where(r => r.ParentId == 0).ToList();
            foreach (var parentNode in parentNodes)
            {
                SetChildNodes(allNodes, parentNode);
                nodeTree.Add(parentNode);
            }
            return nodeTree;
        }

        private static void SetChildNodes(List<TreeResponse> allNodes, TreeResponse parentNode)
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
