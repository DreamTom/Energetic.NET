using Energetic.NET.Basic.Application.UserService.Dto;
using Energetic.NET.Basic.Application.UserService;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.SharedKernel;
using MapsterMapper;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Application.RoleService.Dto;

namespace Energetic.NET.Basic.Infrastructure.UserService
{
    internal class UserAppService(IUserDomainRepository userDomainRepository, IMapper mapper) : IUserAppService
    {
        public async Task<PaginatedList<UserResponse>> GetPageListAsync(UserQueryRequest userQuery)
        {
            var query = userDomainRepository.GetQueryableSet();
            if (!string.IsNullOrWhiteSpace(userQuery.UserName))
                query = query.Where(u => !string.IsNullOrWhiteSpace(u.UserName) && u.UserName.Contains(userQuery.UserName));
            if (!string.IsNullOrWhiteSpace(userQuery.NickName))
                query = query.Where(u => u.NickName.Contains(userQuery.NickName));
            if (userQuery.Gender != null)
                query = query.Where(u => u.Gender == userQuery.Gender);

            return await query.Include(u => u.Roles).ToPageListAsync<User, UserResponse>(mapper, userQuery.PageNumber,
                userQuery.PageSize, userQuery.PropName, userQuery.OrderBy);
        }

        public async Task<UserResourceResponse> GetUserResourcesAsync(long userId)
        {
            var nodeTree = new List<UserResourceTreeResponse>();
            var user = await userDomainRepository.GetUserIncludeRolesAndResourcesAsync(userId);
            var allResources = user.Roles.SelectMany(u => u.Resources);
            var menus = allResources.Where(u => u.Type != ResourceType.Button);
            var allNodes = mapper.Map<List<UserResourceTreeResponse>>(menus);
            var parentNodes = allNodes.Where(a => a.ParentId == 0);
            foreach (var parentNode in parentNodes)
            {
                SetChildNodes(allNodes, parentNode);
                nodeTree.Add(parentNode);
            }
            var permissions = allResources.Except(menus).Where(p => !string.IsNullOrWhiteSpace(p.Code));
            return new UserResourceResponse
            {
                Id = userId,
                Menus = nodeTree,
                Permissions = permissions.Select(p => p.Code).Distinct().ToList(),
            };
        }

        private static void SetChildNodes(List<UserResourceTreeResponse> allNodes, UserResourceTreeResponse parentNode)
        {
            var childNodes = allNodes.Where(x => x.ParentId == parentNode.TempId).ToList();
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
