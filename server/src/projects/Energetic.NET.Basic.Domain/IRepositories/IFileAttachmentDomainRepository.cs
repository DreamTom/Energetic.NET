using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Domain.IRepositories
{
    public interface IFileAttachmentDomainRepository : IBaseRepository<FileAttachment>
    {
        Task<FileAttachment?> FindFileAsync(string hashCode, long fileSize);
    }
}
