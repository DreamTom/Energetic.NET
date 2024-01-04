using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.User.Dto
{
    public class RegRequest(string nickName, string verificationCode, string captchaId)
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string NickName { get; init; } = nickName;

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string VerificationCode { get; init; } = verificationCode;

        public string CaptchaId { get; init; } = captchaId;

        public string? SecondCode { get; set; }

        public Gender Gender { get; set; }

        public RegisterWay RegisterWay { get; set; }
    }
}
