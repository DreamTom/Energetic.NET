using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public record RegRequest(string UserName, string Password, string NickName, string VerificationCode, string CaptchaId)
    {
        public string? RealName { get; set; }

        public Gender Gender { get; set; }
    }
}
