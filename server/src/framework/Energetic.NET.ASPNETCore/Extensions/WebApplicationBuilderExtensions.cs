using Energetic.NET.ASPNETCore.ConfigOptions;
using Energetic.NET.ASPNETCore.Filters;
using Energetic.NET.ASPNETCore.Handlers;
using Energetic.NET.ASPNETCore.Security;
using Energetic.NET.Common.Helpers;
using Energetic.NET.Common.JsonConverters;
using Energetic.NET.Infrastructure;
using Energetic.NET.Jwt;
using Energetic.NET.SharedKernel;
using EnergeticCms.WebApi.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Energetic.NET.ASPNETCore.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// 框架内置服务注入
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureEnergeticNetServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        .WriteTo.Console());

            var services = builder.Services;
            IConfiguration configuration = builder.Configuration;

            App.InitConfiguration(configuration);
            services.AddControllers();
            services.AddConfigOptions<DbConnectionConfigOptions>();
            services.AddConfigOptions<SwaggerConfigOptions>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddJwtAuthentication(App.GetConfigOptions<JwtConfigOptions>());
            services.AddSwaggerConfig();
            // 跨域参考：https://learn.microsoft.com/zh-cn/aspnet/core/security/cors?view=aspnetcore-8.0
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    var corsConfig = App.GetConfigOptions<CorsConfigOptions>();
                    policy.WithOrigins(corsConfig.AllowOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
            services.AddFluentValidationAutoValidation();
            services.Configure<JsonOptions>(options =>
            {
                // 设置时间格式。而非“2008-08-08T08:08:08”这样的格式
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter(DateTimeTemplate.DateWithSeconds));
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
                options.Filters.Add<UnitOfWorkFilter>();
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = action => action.ModelState.Formatter();
            });
            //services.AddExceptionHandler<ApiExceptionHandler>();
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
            services.AddValidatorsFromAssemblies(assemblies);
            services.AddAllDbContexts(assemblies);
            services.RunModuleInitializers(assemblies);
        }
    }
}
