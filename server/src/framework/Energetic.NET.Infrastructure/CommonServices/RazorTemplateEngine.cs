using EasyCaching.Core;
using Energetic.NET.SharedKernel.ICommonServices;
using RazorEngineCore;

namespace Energetic.NET.Infrastructure.CommonServices
{
    internal class RazorTemplateEngine(IEasyCachingProvider cachingProvider, IRazorEngine razorEngine) : IRazorTemplateEngine
    {
        public async Task<string> RenderAsync<T>(string templatePath, T model) where T : RazorEngineTemplateBase
        {
            CheckTemplateExists(templatePath);
            var templateCode = Path.GetFileNameWithoutExtension(templatePath).GetHashCode();
            var cacheKey = templateCode.ToString();
            var templateCache = await cachingProvider.GetAsync(cacheKey, async () =>
            {
                var content = await File.ReadAllTextAsync(templatePath);
                return razorEngine.Compile<RazorEngineTemplateBase<T>>(content);
            }, TimeSpan.FromDays(1));
            return await templateCache.Value.RunAsync(instance =>
            {
                instance.Model = model;
            });
        }

        public async Task<string> RenderAsync(string templatePath, object model)
        {
            CheckTemplateExists(templatePath);
            var templateCode = Path.GetFileNameWithoutExtension(templatePath).GetHashCode();
            var cacheKey = templateCode.ToString();
            var templateCache = await cachingProvider.GetAsync(cacheKey, async () =>
            {
                var content = await File.ReadAllTextAsync(templatePath);
                return razorEngine.Compile(content);
            }, TimeSpan.FromDays(1));
            return await templateCache.Value.RunAsync(model);
        }

        private static void CheckTemplateExists(string templatePath)
        {
            if (!File.Exists(templatePath))
                throw new FileNotFoundException(nameof(templatePath) + "模板文件未找到");
        }
    }
}
