using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public class UserResponse : BaseAuditableResponse
    {
        public long Id { get; set; }

        public string? UserName { get; set; }

        public string NickName { get; set; } = null!;

        public string? RealName { get; set; }

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public long? AvatarId { get; set; }

        public bool IsEnable { get; set; }

        public List<SimpleRoleResponse> Roles { get; set; } = [];

        public List<long> RoleIds => Roles.Select(r => r.Id).ToList();

        public string FirstLetterName => !string.IsNullOrWhiteSpace(RealName) ? RealName.First().ToString() : NickName.First().ToString();
    }

    public class SimpleRoleResponse
    {
        public long Id { get; set; }

        public required string Name { get; set; }
    }
}
