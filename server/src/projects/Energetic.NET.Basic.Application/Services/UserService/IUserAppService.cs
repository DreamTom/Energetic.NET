using Energetic.NET.Basic.Application.UserService.Dto;

namespace Energetic.NET.Basic.Application.UserService
{
    public interface IUserAppService
    {
        Task<PaginatedList<UserResponse>> GetPageListAsync(UserQueryRequest userQuery);

        Task<UserResourceResponse> GetUserResourcesAsync(long userId);
    }
}
