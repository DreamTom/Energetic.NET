﻿using Energetic.NET.Basic.Application.User.Dto;
using FluentValidation;

namespace Energetic.NET.Basic.Infrastructure.AppServices.User.Validators
{
    public class RegRequestValidator : AbstractValidator<RegRequest>
    {
        public RegRequestValidator()
        {
            RuleFor(x => x.NickName).NotEmpty().WithMessage("昵称不能为空");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("性别不合法");
            RuleFor(x => x.VerificationCode).NotEmpty().WithMessage("验证码不能为空");
            RuleFor(x => x.CaptchaId).NotEmpty().WithMessage("验证码标识不能为空");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("密码长度不能少于6位");
        }
    }
}
