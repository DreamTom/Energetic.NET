using Energetic.NET.Common;
using Energetic.NET.Infrastructure.EFCore;
using Energetic.NET.SharedKernel;
using Microsoft.Extensions.DependencyInjection;

namespace Energetic.NET.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<IDateTimeService, DateTimeService>();
        }
    }
}
