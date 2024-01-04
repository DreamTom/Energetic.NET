using Energetic.NET.Basic.Application.User.Dto;
using Energetic.NET.Basic.Domain.Enums;
using FluentValidation;
using Lazy.Captcha.Core;

namespace Energetic.NET.Basic.Infrastructure.AppServices.User.Validators
{
    public class RegRequestValidator : AbstractValidator<RegRequest>
    {
        public RegRequestValidator(ICaptcha captcha)
        {
            RuleFor(x => x.NickName).NotEmpty().WithMessage("昵称不能为空");
            RuleFor(x => x.RegisterWay).IsInEnum().WithMessage("注册方式不支持");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("性别不合法");
            When(x => x.RegisterWay == RegisterWay.UserName, () =>
            {
                RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空");
                RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("密码长度不能少于6位");
            });
            When(x => x.RegisterWay == RegisterWay.PhoneNumber, () =>
            {
                RuleFor(x => x.PhoneNumber).NotEmpty()
                     .Matches("^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$")
                     .WithMessage("手机号不合法");
                RuleFor(x => x.SecondCode)
                     .NotEmpty().WithMessage("手机验证码不能为空");
            });
            When(x => x.RegisterWay == RegisterWay.EmailAddress, () =>
            {
                RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress()
                     .WithMessage("邮箱不合法");
                RuleFor(x => x.SecondCode)
                     .NotEmpty().WithMessage("邮箱验证码不能为空");
            });
            RuleFor(x => x.VerificationCode).NotEmpty().WithMessage("验证码不能为空");
            RuleFor(x => x.CaptchaId).NotEmpty().WithMessage("验证码标识不能为空");
        }
    }
}
