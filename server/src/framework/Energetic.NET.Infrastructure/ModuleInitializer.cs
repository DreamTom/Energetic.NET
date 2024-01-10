using Energetic.NET.Common;
using Energetic.NET.Infrastructure.CommonServices;
using Energetic.NET.Infrastructure.EFCore;
using Energetic.NET.SharedKernel;
using Energetic.NET.SharedKernel.ICommonServices;
using IdGen.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RazorEngineCore;

namespace Energetic.NET.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddIdGen(App.GetConfigOptions<DbConnectionConfigOptions>().GeneratorId);
            services.AddEasyCaching(option =>
            {
                option.UseInMemory();
            });
            services.AddCaptcha();
            services.AddSingleton<IRazorEngine, RazorEngine>();
            services.AddScoped<IRazorTemplateEngine, RazorTemplateEngine>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
