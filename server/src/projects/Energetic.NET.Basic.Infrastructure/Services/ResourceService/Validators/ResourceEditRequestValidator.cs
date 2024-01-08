using Energetic.NET.Basic.Application.ResourceService.Dto;
using Energetic.NET.Basic.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Infrastructure.ResourceService.Validators
{
    public class ResourceEditRequestValidator : AbstractValidator<ResourceEditRequest>
    {
        public ResourceEditRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("菜单名称不能为空");
            RuleFor(x => x.Type).IsInEnum().WithMessage("菜单类型不支持");
            When(c => c.Type == ResourceType.Menu || c.Type == ResourceType.Folder, () =>
            {
                RuleFor(x => x.RoutePath).NotEmpty().WithMessage("路由地址不能为空");
            });
            When(c => c.Type == ResourceType.Button, () =>
            {
                RuleFor(x => x.ApiUrl).NotEmpty().WithMessage("接口地址不能为空");
                RuleFor(x => x.Code).NotEmpty().WithMessage("权限标识不能为空");
                RuleFor(x => x.RequestMethod).IsInEnum().WithMessage("请求方法不支持");
                RuleFor(x => x.ReleationIds).NotEmpty().WithMessage("上级菜单不能为空");
            });
        }
    }
}
