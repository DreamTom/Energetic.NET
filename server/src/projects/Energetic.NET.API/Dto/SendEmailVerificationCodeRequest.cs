namespace Energetic.NET.API.Dto
{
    public record SendEmailVerificationCodeRequest(string CaptchaId, string VerificationCode, string EmailAddress, OperationType OperationType)
    {
    }
}
