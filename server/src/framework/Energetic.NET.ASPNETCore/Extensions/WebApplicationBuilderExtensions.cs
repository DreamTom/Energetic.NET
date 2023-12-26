using Energetic.NET.ASPNETCore.ConfigOptions;
using Energetic.NET.ASPNETCore.Security;
using Energetic.NET.Common.Helpers;
using Energetic.NET.Common.JsonConverters;
using Energetic.NET.Infrastructure;
using Energetic.NET.Jwt;
using Energetic.NET.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var services = builder.Services;
            IConfiguration configuration = builder.Configuration;

            App.InitConfiguration(configuration);

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
            services.AddControllers();
            services.Configure<JsonOptions>(options =>
            {
                // 设置时间格式。而非“2008-08-08T08:08:08”这样的格式
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter(DateTimeTemplate.DateWithSeconds));
            });
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
            services.AddAllDbContexts(assemblies);
            services.RunModuleInitializers(assemblies);
        }
    }
}
