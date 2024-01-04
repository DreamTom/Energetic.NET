namespace Energetic.NET.API.Dto
{
    public record SendSmsVerificationCodeRequest(string CaptchaId, string VerificationCode, string PhoneNumber, OperationType OperationType)
    {

    }
}
