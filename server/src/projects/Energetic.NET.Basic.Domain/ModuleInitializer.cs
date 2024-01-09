using Energetic.NET.Basic.Domain.Services;
using Energetic.NET.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Energetic.NET.Basic.Domain
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<UserDomainService>();
            services.AddScoped<RoleDomainService>();
        }
    }
}
