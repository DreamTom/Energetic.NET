using Energetic.NET.Infrastructure.ConfigOptions;
using Energetic.NET.SharedKernel.ICommonServices;
using Microsoft.Extensions.Options;

namespace Energetic.NET.Infrastructure.CommonServices
{
    public class LocalStorageService(IOptions<LocalStorageConfigOptions> localStorage) : IStorageService
    {
        public async Task<string> SaveAsync(string subDir, Stream content, CancellationToken cancellationToken)
        {
            var localConfig = localStorage.Value;
            if (subDir.StartsWith('/'))
                throw new ArgumentException("subDir should not start with /", nameof(subDir));

            string workDir = localConfig.SavePath;
            string fullPath = Path.Combine(workDir, subDir);
            string? fullDir = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(fullDir) && fullDir != null)
                Directory.CreateDirectory(fullDir);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            using var outStream = File.OpenWrite(fullPath);
            await content.CopyToAsync(outStream, cancellationToken);
            return fullPath;
        }
    }
}
