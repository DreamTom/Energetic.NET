using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Energetic.NET.ASPNETCore")]
namespace Energetic.NET.Infrastructure
{
    public sealed class App
    {
        private static IServiceProvider? _serviceProvider;
        private static IConfiguration? _configuration;

        private App() { }

        internal static void InitServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal static void InitConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static IServiceProvider GetServiceProvider()
        {
            if (_serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(_serviceProvider));
            }
            return _serviceProvider;
        }

        internal static IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                throw new ArgumentNullException(nameof(_configuration));
            }
            return _configuration;
        }

        public static T GetRequiredService<T>() where T : class
        {
            return GetServiceProvider().GetRequiredService<T>();
        }

        public static TOptions GetConfigOptions<TOptions>() where TOptions : class
        {
            var configuration = GetConfiguration();
            var className = typeof(TOptions).Name;
            string key = className.Replace("Options", "");
            var configOptions = configuration.GetSection(key).Get<TOptions>();
            if (configOptions == null)
            {
                key = className;
                configOptions = configuration.GetSection(key).Get<TOptions>();
            }
            return configOptions ?? throw new ArgumentNullException($"{key} not found in appsettings.json");
        }
    }
}
