using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.User.Dto
{
    public class RegRequest(string userName, string password, string nickName, Gender gender, RegisterWay registerWay)
    {
        public string UserName { get; init; } = userName;

        public string Password { get; init; } = password;

        public string NickName { get; init; } = nickName;

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public Gender Gender { get; init; } = gender;

        public RegisterWay RegisterWay { get; init; } = registerWay;
    }
}
