using Energetic.NET.SharedKernel;
using Energetic.NET.SharedKernel.IModels;
using System.IdentityModel.Tokens.Jwt;

namespace Energetic.NET.ASPNETCore.Security
{
    internal class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public IUserInfo? CurrentUser => GetCurrentUserInfo();

        public IUserInfo? GetCurrentUserInfo()
        {
            var httpContext = _contextAccessor?.HttpContext;
            if (httpContext  == null) 
                return null;

            var identity = httpContext.User.Identity;
            if (identity is { IsAuthenticated: false })
                return null;

            var claims = httpContext.User.Claims;

            long id = long.Parse(claims.Single(c => c.Type == JwtRegisteredClaimNames.Sid).Value);
            var tenantIdStr = claims.SingleOrDefault(c => c.Type == "tenantId")?.Value;
            long tenantId = 0;
            if (!string.IsNullOrWhiteSpace(tenantIdStr))
                tenantId = long.Parse(tenantIdStr);
            string userName = claims.Single(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value;
            string realName = claims.Single(c => c.Type == JwtRegisteredClaimNames.Name).Value;
            int jwtVersion = int.Parse(claims.Single(c => c.Type == "jwtVersion").Value);
            return new WebUserInfo(id, userName, realName, tenantId, jwtVersion);
        }
    }
}
