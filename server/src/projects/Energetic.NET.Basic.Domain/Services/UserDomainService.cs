using EasyCaching.Core;
using Energetic.NET.Basic.Domain.Constants;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Basic.Domain.Models;
using Energetic.NET.Jwt;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Energetic.NET.Basic.Domain.Services
{
    public class UserDomainService(IUserDomainRepository userDomainRepository,
            ITokenService tokenService,
            IEasyCachingProvider cachingProvider,
            IOptions<JwtConfigOptions> jwtConfigOption)
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string? Token, User? User)> LoginByUserNameAndPasswordAsync(string userName, string password)
        {
            var user = await userDomainRepository.FindByUserNameAsync(userName);
            if (user == null || !user.CheckPassword(password))
                return (false, null, null);
            string token = BuildToken(user);
            return (true, token, user);
        }

        /// <summary>
        /// 手机号和验证码登录，未注册自动注册
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="secondCode">验证码</param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string? Token, User? User)> LoginByPhoneNumberAsync(string phoneNumber, string secondCode)
        {
            string key = CacheKey.LoginByPhoneNumberPrefix + phoneNumber;
            if (!CheckVerifyCode(key, secondCode))
                return (false, null, null);
            var user = await userDomainRepository.FindByPhoneNumberAsync(phoneNumber);
            user ??= await userDomainRepository.RegisterByPhoneNumberAsync(phoneNumber);
            string token = BuildToken(user);
            return (true, token, user);
        }

        /// <summary>
        /// 邮箱和验证码登录，未注册自动注册
        /// </summary>
        /// <param name="emailAddress">邮箱地址</param>
        /// <param name="secondCode">验证码</param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string? Token, User? User)> LoginByEmailAddressAsync(string emailAddress, string secondCode)
        {
            string key = CacheKey.LoginByEmailAddressPrefix + emailAddress;
            if (!CheckVerifyCode(key, secondCode))
                return (false, null, null);
            var user = await userDomainRepository.FindByEmailAdressAsync(emailAddress);
            user ??= await userDomainRepository.RegisterByEmailAddressAsync(emailAddress);
            string token = BuildToken(user);
            return (true, token, user);
        }

        private bool CheckVerifyCode(string key, string verifyCode)
        {
            var code = cachingProvider.Get<string>(key);
            return code.HasValue && code.Value.Equals(verifyCode);
        }

        private string BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sid, user.Id.ToString(), ClaimValueTypes.Integer64),
                new(JwtClaimNames.NickName, user.NickName),
            };
            if (!string.IsNullOrWhiteSpace(user.UserName))
                claims.Add(new(JwtClaimNames.UserName, user.UserName));
            if (!string.IsNullOrWhiteSpace(user.RealName))
                claims.Add(new(JwtRegisteredClaimNames.Name, user.RealName));
            return tokenService.BuildToken(claims, jwtConfigOption.Value);
        }
    }
}
