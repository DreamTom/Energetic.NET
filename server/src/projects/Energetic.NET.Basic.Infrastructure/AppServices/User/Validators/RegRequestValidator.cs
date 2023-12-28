using Energetic.NET.Basic.Application.User.Dto;
using FluentValidation;

namespace Energetic.NET.Basic.Infrastructure.AppServices.User.Validators
{
    public class RegRequestValidator : AbstractValidator<RegRequest>
    {
        public RegRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(x => x.NickName).NotEmpty().WithMessage("昵称不能为空");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("密码长度不能少于6位");
            RuleFor(x => x.PhoneNumber).Matches("^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$")
                .WithMessage("手机号不合法")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
            RuleFor(x => x.EmailAddress).EmailAddress()
                .WithMessage("邮箱不合法")
                .When(x => !string.IsNullOrWhiteSpace(x.EmailAddress));
        }
    }
}
