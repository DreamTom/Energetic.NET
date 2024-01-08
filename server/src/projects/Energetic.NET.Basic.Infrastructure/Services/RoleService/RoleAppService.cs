using Energetic.NET.Basic.Application.RoleService;
using Energetic.NET.Basic.Application.RoleService.Dto;
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

            return await query.ToPageListAsync<Role, RoleResponse>(mapper, roleQuery.PageNumber, roleQuery.PageSize);
        }
    }
}
