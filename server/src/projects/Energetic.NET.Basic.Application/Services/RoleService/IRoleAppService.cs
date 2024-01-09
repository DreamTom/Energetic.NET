using Energetic.NET.Basic.Application.RoleService.Dto;
using Energetic.NET.Basic.Application.Services.RoleService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.RoleService
{
    public interface IRoleAppService
    {
        Task<PaginatedList<RoleResponse>> GetPageListAsync(RoleQueryRequest roleQuery);

        Task<List<RoleResourceTreeResponse>> GetRoleResourcesTreeAsync(long roleId);
    }
}
