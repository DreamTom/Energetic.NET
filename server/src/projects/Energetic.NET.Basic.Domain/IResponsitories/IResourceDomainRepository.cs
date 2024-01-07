using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Domain.IResponsitories
{
    public interface IResourceDomainRepository
    {
        Task<bool> IsExistsPathAsync(string routePath);

        Task<bool> IsExistsCodeAsync(string code);

        Task<bool> IsExistsApiAsync(string apiUrl, RequestMethod requestMethod);

        Task<List<Resource>> GetResourcesAsync(string? name, string? routePath, string? code);

        Task<List<Resource>> GetResourcesIgnoreButtonsAsync();

        Task<Resource?> FindByIdAsync(long id);
    }
}
