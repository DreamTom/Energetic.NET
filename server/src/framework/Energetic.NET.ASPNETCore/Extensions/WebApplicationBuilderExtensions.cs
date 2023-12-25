using Energetic.NET.ASPNETCore.ConfigOptions;
using Energetic.NET.ASPNETCore.Security;
using Energetic.NET.Infrastructure;
using Energetic.NET.Jwt;
using Energetic.NET.SharedKernel;

namespace Energetic.NET.ASPNETCore.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
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

            services.AddControllers();
        }
    }
}
