using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Common;

namespace Energetic.NET.Basic.Domain.Services
{
    public class FileAttachmentDomainService(IFileAttachmentDomainRepository fileAttachmentDomainRepository,
        IStorageService storageService)
    {
        public async Task<FileAttachment> UploadAsync(Stream content, string fileName, string contentType, CancellationToken cancellationToken)
        {
            string hashCode = content.ComputeSha256Hash();
            long fileSize = content.Length;
            DateTime today = DateTime.Now;
            string subDir = $"{today.Year}/{today.Month}/{today.Day}/{hashCode}/{fileName}";

            var existsFile = await fileAttachmentDomainRepository.FindFileAsync(hashCode, fileSize);
            if (existsFile != null)
                return existsFile;

            content.Position = 0;
            var url = await storageService.SaveAsync(subDir, content, cancellationToken);
            return new FileAttachment(fileName, hashCode, url, contentType, fileSize);
        }
    }
}
