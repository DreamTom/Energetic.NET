using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Energetic.NET.Jwt
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, JwtConfigOptions jwtConfig)
        {
            byte[] secKeyBytes = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);
            var securityKey = new SymmetricSecurityKey(secKeyBytes);
            var builder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = securityKey,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwtConfig.Audience,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = async (context) =>
                        {
                            if (string.IsNullOrWhiteSpace(context.Token))
                            {
                                context.HttpContext.Request.Headers.TryGetValue("token", out var token);
                                context.Token = token;
                            }
                            await Task.CompletedTask;
                        }
                    };
                });
            return builder;
        }
    }
}
