using Energetic.NET.SharedKernel.IModels;

namespace Energetic.NET.ASPNETCore
{
    public sealed class WebUserInfo(long id, string nickName) : IUserInfo
    {
        public long Id { get; } = id;

        public string? UserName { get; set; }

        public long TenantId { get; set; }

        public int JwtVersion { get; set; }

        public string NickName { get; } = nickName;

        public string? RealName { get; set; }
    }
}
