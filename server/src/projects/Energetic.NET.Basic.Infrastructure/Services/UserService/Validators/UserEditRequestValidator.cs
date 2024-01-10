using Energetic.NET.Basic.Application.UserService.Dto;
using FluentValidation;

namespace Energetic.NET.Basic.Infrastructure.Services.UserService.Validators
{
    public class UserEditRequestValidator: AbstractValidator<UserEditRequest>
    {
        public UserEditRequestValidator()
        {
            RuleFor(x => x.NickName).NotEmpty().WithMessage("昵称不能为空");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("性别不合法");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(x => x.UserName).MinimumLength(4).WithMessage("用户名长度不能少于4位");
        }
    }
}
