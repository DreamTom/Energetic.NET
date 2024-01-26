using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.ICommonServices
{
    public interface IClientService
    {
        string GetClientIpAddress();

        string GetOperationSystemInfo();

        string GetBrowserInfo();

        string GetUserAgent();
    }
}
