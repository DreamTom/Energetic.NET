﻿using Energetic.NET.Common;
using Energetic.NET.Infrastructure.EFCore;
using Energetic.NET.SharedKernel;
using IdGen.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}
