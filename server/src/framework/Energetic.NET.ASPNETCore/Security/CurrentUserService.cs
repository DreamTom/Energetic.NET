using Energetic.NET.Jwt;
using Energetic.NET.SharedKernel;
using Energetic.NET.SharedKernel.IModels;
using System.IdentityModel.Tokens.Jwt;

namespace Energetic.NET.ASPNETCore.Security
{
    internal class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
    {

        public IUserInfo? CurrentUser => GetCurrentUserInfo();

        public IUserInfo? GetCurrentUserInfo()
        {
            var httpContext = contextAccessor?.HttpContext;
            if (httpContext  == null) 
                return null;

            var identity = httpContext.User.Identity;
            if (identity is { IsAuthenticated: false })
                return null;

            var claims = httpContext.User.Claims;

            long id = long.Parse(claims.Single(c => c.Type == JwtRegisteredClaimNames.Sid).Value);
            //var tenantIdStr = claims.SingleOrDefault(c => c.Type == "tenantId")?.Value;
            //if (!string.IsNullOrWhiteSpace(tenantIdStr))
            //    tenantId = long.Parse(tenantIdStr);
            var userName = claims.SingleOrDefault(c => c.Type == JwtClaimNames.UserName)?.Value;
            string nickName = claims.Single(c => c.Type == JwtClaimNames.NickName).Value;
            var realName = claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            //int jwtVersion = int.Parse(claims.Single(c => c.Type == "jwtVersion").Value);
            return new WebUserInfo(id, nickName)
            {
                JwtVersion = 0,
                UserName = userName,
                TenantId = 0,
                RealName = realName,
            };
        }
    }
}
