using Energetic.NET.API.Models;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authorization;

namespace Energetic.NET.API.Controllers
{
    /// <summary>
    /// 通用服务
    /// </summary>
    /// <param name="captcha"></param>
    [Route("api/common")]
    public class CommonController(ICaptcha captcha) : BaseController
    {
        /// <summary>
        /// 图形验证码生成
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("verificationImg")]
        public ActionResult<CaptchaResponseDto> VerificationImg()
        {
            string captchaId = Guid.NewGuid().ToString("N");
            var captchaInfo = captcha.Generate(captchaId);
            return Ok(new CaptchaResponseDto
            {
                Img = captchaInfo.Base64,
                CaptchaId = captchaId,
            });
        }
    }
}
