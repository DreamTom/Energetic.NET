using Energetic.NET.Basic.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class FileAttachmentRepository(BasicDbContext basicDbContext) :
        BaseRepository<FileAttachment>(basicDbContext), IFileAttachmentDomainRepository
    {
        public Task<FileAttachment?> FindFileAsync(string hashCode, long fileSize)
        {
            return GetQueryableSet().SingleOrDefaultAsync(f => f.HashCode == hashCode && f.Size == fileSize);
        }
    }
}
