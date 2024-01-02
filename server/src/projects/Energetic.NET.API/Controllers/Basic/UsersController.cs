using Energetic.NET.Basic.Application.User.Dto;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IResponsitories;
using Energetic.NET.Basic.Domain.Services;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authorization;

namespace Energetic.NET.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [UnitOfWork(typeof(BasicDbContext))]
    [Route("api/users")]
    public class UsersController(IUserDomainRepository userDomainRepository,
        IEasyCachingProvider easyCaching,
        BasicDbContext basicDbContext,
        UserDomainService userDomainService) : BaseController
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="regRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegRequest regRequest)
        {
            var user = new User(regRequest.RegisterWay, regRequest.NickName);
            if (regRequest.RegisterWay == RegisterWay.UserName)
            {
                var existsUser = await userDomainRepository.FindByUserNameAsync(regRequest.UserName);
                if (existsUser != null)
                    return ValidateFailed("用户名已存在");
                user.AddByUserName(regRequest.UserName, regRequest.Password, regRequest.Gender);
            }
            else if (regRequest.RegisterWay == RegisterWay.EmailAddress)
            {
                var code = await easyCaching.GetAsync<string>($"register_by_emailAddress_{regRequest.EmailAddress}");
                if (code.Value != regRequest.VerificationCode)
                    return ValidateFailed("验证码不正确");
                var existsUser = await userDomainRepository.FindByEmailAdressAsync(regRequest.EmailAddress);
                if (existsUser != null)
                    return ValidateFailed("邮箱已存在");
                user.SetEmailAddress(regRequest.EmailAddress);
            }
            else if (regRequest.RegisterWay == RegisterWay.PhoneNumber)
            {
                var code = await easyCaching.GetAsync<string>($"register_by_phoneNumber_{regRequest.EmailAddress}");
                if (code.Value != regRequest.VerificationCode)
                    return ValidateFailed("验证码不正确");
                var existsUser = await userDomainRepository.FindByEmailAdressAsync(regRequest.EmailAddress);
                if (existsUser != null)
                    return ValidateFailed("邮箱已存在");
                user.SetPhoneNumber(regRequest.PhoneNumber);
            }
            await basicDbContext.AddAsync(user);
            return Ok(user.Id);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromServices] ICaptcha captcha, LoginRequest loginRequest)
        {
            if (loginRequest.LoginWay == RegisterWay.UserName)
            {
                if (!captcha.Validate(loginRequest.CaptchaId, loginRequest.VerificationCode))
                    return ValidateFailed("验证码错误");
                var (IsSuccess, Token) = await userDomainService.LoginByUserNameAndPasswordAsync(loginRequest.UserName, loginRequest.Password);
                if (!IsSuccess)
                    return ValidateFailed("用户名或密码错误");
                return Ok(new LoginResponse
                {
                    UserName = loginRequest.UserName,
                    Token = Token
                });
            }
            return Ok();
        }
    }
}
