using Energetic.NET.Basic.Application;

namespace Energetic.NET.API.Dto
{
    public record SendSmsVerificationCodeRequest(string CaptchaId, string VerificationCode, string PhoneNumber, VerificationOperationType OperationType)
    {

    }
}
