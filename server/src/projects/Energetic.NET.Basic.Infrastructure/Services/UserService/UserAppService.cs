using Energetic.NET.Basic.Application.UserService.Dto;
using Energetic.NET.Basic.Application.UserService;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.SharedKernel;
using MapsterMapper;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.Services;
using Energetic.NET.Basic.Application.Services.UserService.Dto;

namespace Energetic.NET.Basic.Infrastructure.UserService
{
    internal class UserAppService(IUserDomainRepository userDomainRepository,
        IUserLoginHistoryRepository userLoginHistoryRepository,
        UserDomainService userDomainService, IMapper mapper) : IUserAppService
    {
        public async Task<PaginatedList<UserResponse>> GetPageListAsync(UserQueryRequest userQuery)
        {
            var query = userDomainRepository.GetQueryableSet().Where(u => !u.IsAdmin);
            if (!string.IsNullOrWhiteSpace(userQuery.UserName))
                query = query.Where(u => !string.IsNullOrWhiteSpace(u.UserName) && u.UserName.Contains(userQuery.UserName));
            if (!string.IsNullOrWhiteSpace(userQuery.NickName))
                query = query.Where(u => u.NickName.Contains(userQuery.NickName));
            if (userQuery.Gender != null)
                query = query.Where(u => u.Gender == userQuery.Gender);

            return await query.Include(u => u.Roles).ToPageListAsync<User, UserResponse>(mapper, userQuery.GetPaginatedQuery());
        }

        public async Task<UserResourceResponse> GetUserResourcesAsync(long userId)
        {
            var nodeTree = new List<UserResourceTreeResponse>();
            var allResources = await userDomainService.GetUserResourcesAsync(userId);
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

        public async Task<PaginatedList<UserLoginHistoryResponse>> GetUserLoginHistoriesAsync(UserLoginHistoryQueryRequest userLoginHistoryQuery)
        {
            var query = userLoginHistoryRepository.GetQueryableSet();
            if (!string.IsNullOrWhiteSpace(userLoginHistoryQuery.LoginAccount))
                query = query.Where(q => q.LoginAccount == userLoginHistoryQuery.LoginAccount);
            if (userLoginHistoryQuery.RangeTime.Length != 0)
                query = query.Where(q => q.CreatedTime >= userLoginHistoryQuery.RangeTime[0] && q.CreatedTime <= userLoginHistoryQuery.RangeTime[1]);
            return await query.ToPageListAsync<UserLoginHistory, UserLoginHistoryResponse>(mapper, userLoginHistoryQuery.GetPaginatedQuery());
        }
    }
}
