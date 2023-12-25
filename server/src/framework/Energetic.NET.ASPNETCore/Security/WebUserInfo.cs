using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.ASPNETCore
{
    public sealed class WebUserInfo(long id, string userName, string realName, long tenantId, int jwtVersion) : IUserInfo
    {
        public long Id { get; } = id;

        public string UserName { get; } = userName;

        public long TenantId { get; } = tenantId;

        public int JwtVersion { get; } = jwtVersion;

        public string RealName { get; } = realName;
    }
}
