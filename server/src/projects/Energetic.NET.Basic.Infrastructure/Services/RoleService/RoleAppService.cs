using Energetic.NET.Basic.Application.ResourceService.Dto;
using Energetic.NET.Basic.Application.RoleService;
using Energetic.NET.Basic.Application.RoleService.Dto;
using Energetic.NET.Basic.Application.Services.RoleService.Dto;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.SharedKernel;
using MapsterMapper;

namespace Energetic.NET.Basic.Infrastructure.RoleService
{
    internal class RoleAppService(IRoleDomainRepository roleDomainRepository, IMapper mapper) : IRoleAppService
    {
        public async Task<PaginatedList<RoleResponse>> GetPageListAsync(RoleQueryRequest roleQuery)
        {
            var query = roleDomainRepository.GetQueryableSet();
            if (!string.IsNullOrWhiteSpace(roleQuery.Name))
                query = query.Where(r => r.Name.Contains(roleQuery.Name));
            if (!string.IsNullOrWhiteSpace(roleQuery.Code))
                query = query.Where(r => r.Code.Contains(roleQuery.Code));
            return await query.ToPageListAsync<Role, RoleResponse>(mapper, roleQuery.PageNumber, roleQuery.PageSize,
                roleQuery.PropName, roleQuery.OrderBy);
        }

        public async Task<List<RoleResourceTreeResponse>> GetRoleResourcesTreeAsync(long roleId)
        {
            var nodeTree = new List<RoleResourceTreeResponse>();
            var role = await roleDomainRepository.GetRoleIncludeResourcesAsync(roleId);
            var allNodes = mapper.Map<List<RoleResourceTreeResponse>>(role.Resources);
            var parentNodes = allNodes.Where(r => r.ParentId == 0);
            foreach (var parentNode in parentNodes)
            {
                SetChildNodes(allNodes, parentNode);
                nodeTree.Add(parentNode);
            }
            return nodeTree;
        }

        private static void SetChildNodes(List<RoleResourceTreeResponse> allNodes, RoleResourceTreeResponse parentNode)
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
    }
}
