using Energetic.NET.Basic.Application;

namespace Energetic.NET.API.Dto
{
    public record SendEmailVerificationCodeRequest(string CaptchaId, string VerificationCode, string EmailAddress, VerificationOperationType OperationType)
    {
    }
}
