﻿using Energetic.NET.Common;
using Energetic.NET.Infrastructure.CommonServices;
using Energetic.NET.Infrastructure.EFCore;
using Energetic.NET.SharedKernel;
using Energetic.NET.SharedKernel.ICommonServices;
using IdGen.DependencyInjection;
using MailKitSimplified.Sender;
using Mapster;
using MapsterMapper;
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
            services.AddIdGen(124, () => new IdGen.IdGeneratorOptions
            {

            });
            services.AddEasyCaching(option =>
            {
                option.UseInMemory();
            });
            services.AddSingleton(new TypeAdapterConfig());
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddCaptcha();
            services.AddSingleton<IRazorEngine, RazorEngine>();
            services.AddScoped<IRazorTemplateEngine, RazorTemplateEngine>();
        }
    }
}
