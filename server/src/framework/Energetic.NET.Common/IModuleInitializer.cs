using Microsoft.Extensions.DependencyInjection;

namespace Energetic.NET.Common
{
    /// <summary>
    /// 模块注入
    /// </summary>
    public interface IModuleInitializer
    {
        void Initialize(IServiceCollection services);
    }
}
