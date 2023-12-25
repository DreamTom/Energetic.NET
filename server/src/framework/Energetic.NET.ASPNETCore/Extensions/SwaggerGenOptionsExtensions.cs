using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Energetic.NET.ASPNETCore.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddAuthenticationHeader(this SwaggerGenOptions options)
        {
            var scheme = new OpenApiSecurityScheme
            {
                Description = "Authorization Header. \r\nExample：'Bearer {your token}'",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authorization"
                },
                Scheme = "oauth2",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            };
            options.AddSecurityDefinition("Authorization", scheme);
            var requirement = new OpenApiSecurityRequirement
            {
                [scheme] = new List<string>()
            };
            options.AddSecurityRequirement(requirement);
        }
    }
}
