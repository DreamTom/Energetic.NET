using Energetic.NET.Basic.Application.User.Dto;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IResponsitories;
using Energetic.NET.Basic.Domain.Models;
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
        /// <param name="captcha"></param>
        /// <param name="regRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromServices] ICaptcha captcha, RegRequest regRequest)
        {
            if (!captcha.Validate(regRequest.CaptchaId, regRequest.VerificationCode))
                return ValidateFailed("验证码错误");
            User addUser;
            if (regRequest.RegisterWay == RegisterWay.UserName)
            {
                var existsUser = await userDomainRepository.FindByUserNameAsync(regRequest.UserName);
                if (existsUser != null)
                    return ValidateFailed("用户名已存在");
                addUser = new User(regRequest.RegisterWay, regRequest.NickName);
                addUser.AddByUserName(regRequest.UserName, regRequest.Password, regRequest.Gender);
                _ = await basicDbContext.AddAsync(addUser);
            }
            else if (regRequest.RegisterWay == RegisterWay.PhoneNumber)
            {
                var code = await easyCaching.GetAsync<string>($"Register_By_PhoneNumber_{regRequest.EmailAddress}");
                if (code.Value != regRequest.SecondCode)
                    return ValidateFailed("手机验证码不正确");
                var existsUser = await userDomainRepository.FindByEmailAdressAsync(regRequest.EmailAddress);
                if (existsUser != null)
                    return ValidateFailed("邮箱已存在");
                addUser = await userDomainRepository.RegisterByPhoneNumberAsync(regRequest.PhoneNumber);
            }
            else if (regRequest.RegisterWay == RegisterWay.EmailAddress)
            {
                var code = await easyCaching.GetAsync<string>($"Register_By_EmailAddress_{regRequest.EmailAddress}");
                if (code.Value != regRequest.SecondCode)
                    return ValidateFailed("邮箱验证码不正确");
                var existsUser = await userDomainRepository.FindByEmailAdressAsync(regRequest.EmailAddress);
                if (existsUser != null)
                    return ValidateFailed("邮箱已存在");
                addUser = await userDomainRepository.RegisterByEmailAddressAsync(regRequest.EmailAddress);
            }
            else
            {
                return ValidateFailed("注册方式不支持");
            }
            return Ok(addUser.Id);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="captcha"></param>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromServices] ICaptcha captcha, LoginRequest loginRequest)
        {
            if (!captcha.Validate(loginRequest.CaptchaId, loginRequest.VerificationCode))
                return ValidateFailed("验证码错误");
            User user = null;
            string token = string.Empty;
            if (loginRequest.LoginWay == RegisterWay.UserName)
            {
                var (IsSuccess, Token, User) = await userDomainService.LoginByUserNameAndPasswordAsync(loginRequest.UserName, loginRequest.Password);
                if (!IsSuccess)
                    return ValidateFailed("用户名或密码错误");
                user = User;
                token = Token;
            }
            else if (loginRequest.LoginWay == RegisterWay.PhoneNumber)
            {
                var (IsSuccess, Token, User) = await userDomainService.LoginByPhoneNumberAsync(loginRequest.PhoneNumber, loginRequest.SecondCode);
                if (!IsSuccess)
                    return ValidateFailed("短信验证码错误");
                user = User;
                token = Token;
            }
            else if (loginRequest.LoginWay == RegisterWay.EmailAddress)
            {
                var (IsSuccess, Token, User) = await userDomainService.LoginByEmailAddressAsync(loginRequest.EmailAddress, loginRequest.SecondCode);
                if (!IsSuccess)
                    return ValidateFailed("邮箱验证码错误");
                user = User;
                token = Token;
            }
            return Ok(new LoginResponse
            {
                UserInfo = new UserDetailResponse
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    NickName = user.NickName,
                    RealName = user.RealName,
                    Gender = user.Gender,
                    EmailAddress = user.EmailAddress,
                    PhoneNumber = user.PhoneNumber,
                },
                Token = token
            });
        }
    }
}
