using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public record UserEditRequest(string UserName, string NickName, bool IsEnable)
    {
        public long? AvatarId { get; set; }

        public string? RealName { get; set; }

        public Gender Gender { get; set; }

        public long[] RoleIds { get; set; } = [];
    }
}
