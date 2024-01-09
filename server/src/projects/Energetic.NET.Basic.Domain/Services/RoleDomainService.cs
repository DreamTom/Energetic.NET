using Energetic.NET.Basic.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Domain.Services
{
    public class RoleDomainService(IRoleDomainRepository roleDomainRepository, IResourceDomainRepository resourceDomainRepository)
    {
        public async Task<Role> UpdateRoleResourcesAsync(long roleId, long[] resourceIds)
        {
            var role = await roleDomainRepository.GetRoleIncludeResourcesAsync(roleId);
            if (resourceIds.Length == 0)
            {
                role.SetResources([]);
                return role;
            }
            var resources = await resourceDomainRepository.FindByIdsAsync(resourceIds);
            role.SetResources(resources);
            return role;
        }
    }
}
