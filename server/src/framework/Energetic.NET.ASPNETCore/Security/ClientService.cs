using Energetic.NET.SharedKernel.ICommonServices;
using Microsoft.Net.Http.Headers;

namespace Energetic.NET.ASPNETCore.Security
{
    public class ClientService(IHttpContextAccessor contextAccessor) : IClientService
    {
        public string GetBrowserInfo()
        {
            throw new NotImplementedException();
        }

        public string GetClientIpAddress()
        {
            var ip = contextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(ip))
                ip = contextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            return string.IsNullOrWhiteSpace(ip) ? "unknown" : ip;
        }

        public string GetOperationSystemInfo()
        {
            throw new NotImplementedException();
        }

        public string GetUserAgent()
        {
            return contextAccessor.HttpContext.Request.Headers[HeaderNames.UserAgent].ToString();
        }
    }
}
