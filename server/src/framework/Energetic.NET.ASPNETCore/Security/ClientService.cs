using Energetic.NET.SharedKernel.ICommonServices;
using IPTools.Core;
using MyCSharp.HttpUserAgentParser;

namespace Energetic.NET.ASPNETCore.Security
{
    class ClientService(IHttpContextAccessor contextAccessor) : IClientService
    {
        public string GetBrowserInfo()
        {
            return GetHttpUserAgentInformation().Name ?? string.Empty;
        }

        public string GetClientIpAddress()
        {
            var ip = contextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(ip))
                ip = contextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            return string.IsNullOrWhiteSpace(ip) ? "unknown" : ip;
        }

        public string GetIpLocation()
        {
            var ip = GetClientIpAddress();
            if (ip == "unknown")
                return "unknown";
            if (IsLocalIp(ip))
                return "本机";
            var ipInfo = IpTool.Search(ip);
            if (ipInfo == null)
                return "unknown";
            return ipInfo.Country + " | " + ipInfo.Province + " | " + ipInfo.City + " | " + ipInfo.NetworkOperator;
        }

        public string GetOperatingSystemInfo()
        {
            var platform = GetHttpUserAgentInformation().Platform;
            if (platform != null)
                return platform.Value.Name;
            return string.Empty;
        }

        public string GetUserAgent()
        {
            return contextAccessor.HttpContext?.GetUserAgentString() ?? "";
        }

        private HttpUserAgentInformation GetHttpUserAgentInformation()
        {
            return HttpUserAgentParser.Parse(GetUserAgent());
        }

        private static bool IsLocalIp(string ip)
        {
            if (ip == "0.0.0.1" || ip == "localhost" || ip == "127.0.0.1" || ip == "::1")
                return true;
            return false;
        }
    }
}
