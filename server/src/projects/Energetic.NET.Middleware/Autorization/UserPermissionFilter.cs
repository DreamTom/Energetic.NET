using Energetic.NET.ASPNETCore;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Infrastructure.ConfigOptions;
using Energetic.NET.SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Energetic.NET.Middleware.Authorization
{
    public class UserPermissionFilter(ICurrentUserService currentUserService,
        IUserDomainRepository userDomainRepository,
        IResourceDomainRepository resourceDomainRepository,
        IOptionsSnapshot<AppConfigOptions> appOptions) : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool hasAllowAnonymousAttribute = false;
            bool hasNoPermissionCheckAttribute = false;
            string route = string.Empty;
            if (context.ActionDescriptor is ControllerActionDescriptor actionDesc)
            {
                if (actionDesc.AttributeRouteInfo == null)
                    throw new ArgumentException("该接口未配置路由！");
                route = "/" + actionDesc.AttributeRouteInfo.Template;
                hasAllowAnonymousAttribute = actionDesc.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), true)
                    || actionDesc.EndpointMetadata.Any(e => e.GetType() == typeof(AllowAnonymousAttribute));
                hasNoPermissionCheckAttribute = actionDesc.MethodInfo.IsDefined(typeof(NoPermissionCheckAttribute), true)
                    || actionDesc.EndpointMetadata.Any(e => e.GetType() == typeof(NoPermissionCheckAttribute));
            }
            if (hasAllowAnonymousAttribute)
            {
                return;
            }

            if (hasNoPermissionCheckAttribute)
            {
                return;
            }

            var user = currentUserService.GetCurrentUserInfo();
            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult("用户未登录！");
                return;
            }

            string requestMethod = context.HttpContext.Request.Method.ToUpper();
            if (appOptions.Value.IsDemo && !appOptions.Value.Permissions.Contains(requestMethod))
            {
                context.Result = new ObjectResult(new ErrorResponseResult("演示环境禁止该操作"))
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            if (await userDomainRepository.IsAdminAsync(user.Id))
            {
                return;
            }

            if (!Enum.TryParse(requestMethod, out RequestMethod method))
            {
                var res = new ObjectResult(new ErrorResponseResult("请求方法不支持"))
                {
                    StatusCode = StatusCodes.Status415UnsupportedMediaType
                };
                context.Result = res;
            }
            var resource = await resourceDomainRepository.FindByApiUrlAndRequestMethodAsync(route, method);
            if (resource == null)
            {
                context.Result = new NotFoundObjectResult(new ErrorResponseResult($"请求路由：{requestMethod.ToUpper()} {route} 系统未配置或已禁用！"));
                return;
            }
            if (!await userDomainRepository.IsHasResourceAsync(user.Id, resource.Id))
            {
                context.Result = new ObjectResult(new ErrorResponseResult($"请求路由：{requestMethod.ToUpper()} {route} 无权限访问"))
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}
