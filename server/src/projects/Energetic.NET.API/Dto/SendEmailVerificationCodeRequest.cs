using Energetic.NET.Basic.Application;
using FluentValidation;

namespace Energetic.NET.API.Dto
{
    public record SendEmailVerificationCodeRequest(string CaptchaId, string VerificationCode, string EmailAddress, VerificationOperationType OperationType)
    {
    }

    public class SendEmailVerificationCodeRequestValidator : AbstractValidator<SendEmailVerificationCodeRequest>
    {
        public SendEmailVerificationCodeRequestValidator()
        {
            RuleFor(x => x.CaptchaId).NotEmpty().WithMessage("验证码标识不能为空");
            RuleFor(x => x.VerificationCode).NotEmpty().WithMessage("验证码不能为空");
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("邮箱不能为空");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("邮箱不合法");
        }
    }
}
