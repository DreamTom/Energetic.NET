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

        string GetOperatingSystemInfo();

        string GetBrowserInfo();

        string GetIpLocation();

        string GetUserAgent();
    }
}
