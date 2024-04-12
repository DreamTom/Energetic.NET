using Energetic.NET.Basic.Application.Services.UserService.Dto;
using Energetic.NET.Basic.Application.UserService.Dto;

namespace Energetic.NET.Basic.Application.UserService
{
    public interface IUserAppService
    {
        Task<PaginatedList<UserResponse>> GetPageListAsync(UserQueryRequest userQuery);

        Task<PaginatedList<UserLoginHistoryResponse>> GetUserLoginHistoriesAsync(UserLoginHistoryQueryRequest userLoginHistoryQuery);

        Task<UserResourceResponse> GetUserResourcesAsync(long userId);
    }
}
