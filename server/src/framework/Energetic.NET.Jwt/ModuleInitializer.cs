using Energetic.NET.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Energetic.NET.Jwt
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
