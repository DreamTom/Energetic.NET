using Energetic.NET.Basic.Application.User.Dto;
using Energetic.NET.Basic.Domain.Enums;
using FluentValidation;

namespace Energetic.NET.Basic.Infrastructure.AppServices.User.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.LoginWay).IsInEnum().WithMessage("登录方式不支持");
            When(x => x.LoginWay == RegisterWay.UserName, () =>
            {
                RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空");
                RuleFor(x => x.Password).NotEmpty().WithMessage("密码不能为空");
            });
            When(x => x.LoginWay == RegisterWay.PhoneNumber, () =>
            {
                RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("手机号不能为空");
                RuleFor(x => x.SecondCode)
                     .NotEmpty().WithMessage("手机验证码不能为空");
            });
            When(x => x.LoginWay == RegisterWay.EmailAddress, () =>
            {
                RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("邮箱不能为空");
                RuleFor(x => x.SecondCode)
                     .NotEmpty().WithMessage("邮箱验证码不能为空");
            });
            RuleFor(x => x.VerificationCode).NotEmpty().WithMessage("验证码不能为空");
            RuleFor(x => x.CaptchaId).NotEmpty().WithMessage("验证码标识不能为空");
        }
    }
}
