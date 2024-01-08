using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public class UserDetailResponse
    {
        public long UserId { get; set; }

        public string? UserName { get; set; }

        public string NickName { get; set; } = null!;

        public string? RealName { get; set; }

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public Gender Gender { get; set; }
    }
}
