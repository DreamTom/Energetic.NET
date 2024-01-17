using Energetic.NET.SharedKernel.IModels;
using Energetic.NET.SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Energetic.NET.ASPNETCore
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected virtual IUserInfo CurrentUser
        {
            get
            {
                var userInfo = HttpContext.RequestServices.GetRequiredService<ICurrentUserService>().GetCurrentUserInfo();
                if (userInfo != null)
                    return userInfo;
                else
                    throw new UnauthorizedAccessException("登录已失效");
            }
        }

        [NonAction]
        public virtual string GetClientIpAddress()
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(ip))
                ip = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            return string.IsNullOrWhiteSpace(ip) ? "unknown" : ip;
        }

        /// <summary>
        /// 验证失败响应
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        [NonAction]
        public virtual BadRequestObjectResult ValidateFailed(int errorCode, string message) => new(new ErrorResponseResult(errorCode, message));

        /// <summary>
        /// 验证失败响应
        /// </summary>
        /// <param name="enumValue">错误代码枚举值</param>
        /// <returns></returns>
        [NonAction]
        public virtual BadRequestObjectResult ValidateFailed(Enum enumValue) => new(new ErrorResponseResult(enumValue));

        /// <summary>
        /// 验证失败响应
        /// </summary>
        /// <param name="message">错误消息内容</param>
        /// <returns></returns>
        [NonAction]
        public virtual BadRequestObjectResult ValidateFailed(string message) => new(new ErrorResponseResult(message));

        /// <summary>
        /// 认证失败响应
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        [NonAction]
        public UnauthorizedObjectResult AuthenticateFailed(int errorCode, string message) => new(new ErrorResponseResult(errorCode, message));

        /// <summary>
        /// 认证失败响应
        /// </summary>
        /// <param name="enumValue">错误代码枚举值</param>
        /// <returns></returns>
        [NonAction]
        public UnauthorizedObjectResult AuthenticateFailed(Enum enumValue) => new(new ErrorResponseResult(enumValue));

        /// <summary>
        /// 认证失败响应
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        [NonAction]
        public UnauthorizedObjectResult AuthenticateFailed(string message) => new(new ErrorResponseResult(message));

        /// <summary>
        /// 数据未找到响应
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        [NonAction]
        public NotFoundObjectResult DataNotFound(int errorCode, string message) => new(new ErrorResponseResult(errorCode, message));

        /// <summary>
        /// 数据未找到响应
        /// </summary>
        /// <param name="enumValue">错误代码枚举值</param>
        /// <returns></returns>
        [NonAction]
        public NotFoundObjectResult DataNotFound(Enum enumValue) => new(new ErrorResponseResult(enumValue));

        /// <summary>
        /// 数据未找到响应
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns></returns>
        [NonAction]
        public NotFoundObjectResult DataNotFound(string message) => new(new ErrorResponseResult(message));
    }
}
