using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.ICommonServices
{
    public interface IStorageService
    {
        Task<string> SaveAsync(string subDir, Stream content, CancellationToken cancellationToken);
    }
}
