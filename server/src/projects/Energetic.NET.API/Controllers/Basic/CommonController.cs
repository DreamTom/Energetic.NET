using Energetic.NET.API.Dto;
using Energetic.NET.ASPNETCore.Security;
using Energetic.NET.Basic.Application.EmailService;
using Energetic.NET.Common.Helpers;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Energetic.NET.API.Controllers
{
    /// <summary>
    /// 通用服务
    /// </summary>
    /// <param name="captcha"></param>
    /// <param name="cachingProvider"></param>
    /// <param name="emailAppService"></param>
    [AllowAnonymous]
    [Route("api/common")]
    public class CommonController(ICaptcha captcha, IEasyCachingProvider cachingProvider,
        IEmailAppService emailAppService) : BaseController
    {
        /// <summary>
        /// 图形验证码生成
        /// </summary>
        /// <returns></returns>
        [HttpGet("verificationImg")]
        public ActionResult<CaptchaResponse> VerificationImg()
        {
            string captchaId = Guid.NewGuid().ToString("N");
            var captchaInfo = captcha.Generate(captchaId);
            return Ok(new CaptchaResponse
            {
                Img = captchaInfo.Base64,
                CaptchaId = captchaId,
            });
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="smsVerificationCodeRequest"></param>
        /// <returns></returns>
        [HttpPost("sendSmsVerificationCode")]
        public async Task<ActionResult> SendSmsVerificationCode(SendSmsVerificationCodeRequest smsVerificationCodeRequest)
        {
            if (!captcha.Validate(smsVerificationCodeRequest.CaptchaId, smsVerificationCodeRequest.VerificationCode))
                return ValidateFailed("验证码错误");
            var rndNumber = RandomHelper.GenNumberCode(4);
            await cachingProvider.SetAsync($"{smsVerificationCodeRequest.OperationType}_By_PhoneNumber_{smsVerificationCodeRequest.PhoneNumber}",
                rndNumber, TimeSpan.FromSeconds(120));
            // TODO 短信发送
            return Ok(rndNumber);
        }

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="emailVerificationCodeRequest"></param>
        /// <returns></returns>
        [EnableRateLimiting(RateLimiterPolicy.Fixed)]
        [HttpPost("sendEmailVerificationCode")]
        public async Task<ActionResult> SendEmailVerificationCode(SendEmailVerificationCodeRequest emailVerificationCodeRequest)
        {
            if (!captcha.Validate(emailVerificationCodeRequest.CaptchaId, emailVerificationCodeRequest.VerificationCode))
                return ValidateFailed("验证码错误");
            await emailAppService.SendEmailVerificationCodeAsync(emailVerificationCodeRequest.EmailAddress, emailVerificationCodeRequest.OperationType);
            return Ok();
        }
    }
}
