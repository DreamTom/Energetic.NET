using Energetic.NET.Basic.Application.Email;
using Energetic.NET.Basic.Domain.IResponsitories;
using Energetic.NET.Basic.Infrastructure.AppServices.Email;
using Energetic.NET.Basic.Infrastructure.Responsitories;
using Energetic.NET.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Energetic.NET.Basic.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUserDomainRepository, UserDomainRepository>();
            services.AddScoped<IEmailAppService, EmailAppService>();
        }
    }
}
