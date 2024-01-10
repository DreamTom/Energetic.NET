using Energetic.NET.Basic.Application.RoleService.Dto;

namespace Energetic.NET.Basic.Application.RoleService
{
    public interface IRoleAppService
    {
        Task<PaginatedList<RoleResponse>> GetPageListAsync(RoleQueryRequest roleQuery);

        Task<List<RoleResourceTreeResponse>> GetRoleResourcesTreeAsync(long roleId);
    }
}
