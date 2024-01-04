using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.User.Dto
{
    public record LoginRequest(string VerificationCode, string CaptchaId)
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string? SecondCode { get; set; }

        public RegisterWay LoginWay { get; set; }
    }
}
