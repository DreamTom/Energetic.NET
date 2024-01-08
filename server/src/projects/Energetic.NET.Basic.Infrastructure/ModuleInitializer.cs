using Energetic.NET.Basic.Application.EmailService;
using Energetic.NET.Basic.Application.ResourceService;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Basic.Infrastructure.Responsitories;
using Energetic.NET.Basic.Infrastructure.EmailService;
using Energetic.NET.Basic.Infrastructure.ResourceService;
using Energetic.NET.Common;
using Microsoft.Extensions.DependencyInjection;
using Energetic.NET.Basic.Application.RoleService;
using Energetic.NET.Basic.Infrastructure.RoleService;

namespace Energetic.NET.Basic.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUserDomainRepository, UserDomainRepository>();
            services.AddScoped<IResourceDomainRepository, ResourceDomainRepository>();
            services.AddScoped<IRoleDomainRepository, RoleDomainRepository>();
            services.AddScoped<IEmailAppService, EmailAppService>();
            services.AddScoped<IResourceAppService, ResourceAppService>();
            services.AddScoped<IRoleAppService, RoleAppService>();
        }
    }
}
