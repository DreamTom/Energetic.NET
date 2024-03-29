﻿using Energetic.NET.ASPNETCore.ConfigOptions;
using Energetic.NET.ASPNETCore.Extensions;
using Energetic.NET.ASPNETCore.Security;
using Energetic.NET.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.RateLimiting;

namespace Microsoft.Extensions.Configuration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 选项配置注入
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddConfigOptions<TOptions>(this IServiceCollection services) where TOptions : class
        {
            var configOptions = typeof(TOptions);
            var sectionName = configOptions.Name;
            if (configOptions.Name.EndsWith("Options"))
                sectionName = configOptions.Name.Replace("Options", "");
            services.Configure<TOptions>(App.GetConfiguration().GetSection(sectionName));
        }

        /// <summary>
        /// Swagger配置注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            var swaggerConfig = App.GetConfigOptions<SwaggerConfigOptions>();
            services.AddSwaggerGen(option =>
            {
                option.AddAuthenticationHeader();
                option.SwaggerDoc(swaggerConfig.Version, new OpenApi.Models.OpenApiInfo
                {
                    Title = swaggerConfig.Title,
                    Description = swaggerConfig.Description,
                    Version = swaggerConfig.Version,
                    Contact = new OpenApi.Models.OpenApiContact
                    {
                        Email = swaggerConfig.Contact.Email,
                        Name = swaggerConfig.Contact.Name,
                        Url = new Uri(swaggerConfig.Contact.Url)
                    }
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, swaggerConfig.XmlFileName);
                option.IncludeXmlComments(xmlPath, true);
            });
        }

        /// <summary>
        /// 限流配置
        /// </summary>
        /// <param name="services"></param>
        public static void AddRateLimiterConfig(this IServiceCollection services)
        {
            var limiterConfig = App.GetConfigOptions<RateLimiterConfigOptions>();
            services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(RateLimiterPolicy.Fixed, option =>
                {
                    var fixedConfig = limiterConfig.FixedWindow;
                    option.PermitLimit = fixedConfig.PermitLimit;
                    option.Window = TimeSpan.FromSeconds(fixedConfig.PermitSeconds);
                    option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    option.QueueLimit = fixedConfig.QueueLimit;
                }));
        }
    }
}
