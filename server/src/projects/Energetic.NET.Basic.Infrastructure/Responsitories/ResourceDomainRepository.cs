using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IResponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class ResourceDomainRepository(BasicDbContext basicDbContext) : IResourceDomainRepository
    {
        public Task<List<Resource>> GetResourcesAsync()
        {
            return basicDbContext.Resources.OrderBy(r => r.DisplayOrder).ToListAsync();
        }

        public Task<bool> IsExistsApiAsync(string apiUrl, RequestMethod requestMethod)
        {
            return basicDbContext.Resources.AnyAsync(r => r.ApiUrl == apiUrl && r.RequestMethod == requestMethod
                   && !r.IsMenu && !r.IsFolder);
        }

        public Task<bool> IsExistsCodeAsync(string code)
        {
            return basicDbContext.Resources.AnyAsync(r => r.Code == code && !r.IsMenu && !r.IsFolder);
        }

        public Task<bool> IsExistsPathAsync(string routePath)
        {
            return basicDbContext.Resources.AnyAsync(r => r.RoutePath == routePath && r.IsMenu);
        }
    }
}
