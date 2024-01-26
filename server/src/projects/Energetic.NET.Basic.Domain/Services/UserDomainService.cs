using EasyCaching.Core;
using Energetic.NET.Basic.Domain.Constants;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Jwt;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Energetic.NET.Basic.Domain.Services
{
    public class UserDomainService(IUserDomainRepository userDomainRepository,
            IRoleDomainRepository roleDomainRepository,
            IResourceDomainRepository resourceDomainRepository,
            ITokenService tokenService,
            IEasyCachingProvider cachingProvider,
            IOptions<JwtConfigOptions> jwtConfigOption,
            ICurrentUserService currentUserService,
            IClientService clientService)
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<(LoginFailedResult? LoginFailedResult, string? Token, User? User)> LoginByUserNameAndPasswordAsync(string userName, string password)
        {
            var user = await userDomainRepository.FindByUserNameAsync(userName);
            if (user == null || !user.CheckPassword(password))
            {
                await PublishLoginEventAsync(null, userName, LoginWay.UserName, ResultType.Fail, LoginFailedResult.UserNameOrPasswordIsWrong);
                return (LoginFailedResult.UserNameOrPasswordIsWrong, null, null);
            }
            if (!user.IsEnable)
            {
                await PublishLoginEventAsync(user.Id, userName, LoginWay.UserName, ResultType.Fail, LoginFailedResult.UserIsForbidden);
                return (LoginFailedResult.UserIsForbidden, null, null);
            }
            await PublishLoginEventAsync(user.Id, userName, LoginWay.UserName, ResultType.Success, null);
            string token = BuildToken(user);
            return (null, token, user);
        }

        /// <summary>
        /// 手机号和验证码登录，未注册自动注册
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="secondCode">验证码</param>
        /// <returns></returns>
        public async Task<(LoginFailedResult? LoginFailedResult, string? Token, User? User)> LoginByPhoneNumberAsync(string phoneNumber, string secondCode)
        {
            string key = CacheKey.LoginByPhoneNumberPrefix + phoneNumber;
            if (!CheckVerifyCode(key, secondCode))
            {
                await PublishLoginEventAsync(null, phoneNumber, LoginWay.PhoneNumber, ResultType.Fail, LoginFailedResult.SmsVerificationCodeIsWrong);
                return (LoginFailedResult.SmsVerificationCodeIsWrong, null, null);
            }
            var user = await userDomainRepository.FindByPhoneNumberAsync(phoneNumber);
            if (user != null && !user.IsEnable)
            {
                await PublishLoginEventAsync(user.Id, phoneNumber, LoginWay.PhoneNumber, ResultType.Fail, LoginFailedResult.UserIsForbidden);
                return (LoginFailedResult.UserIsForbidden, null, null);
            }
            user ??= await userDomainRepository.RegisterByPhoneNumberAsync(phoneNumber);
            await PublishLoginEventAsync(user.Id, phoneNumber, LoginWay.PhoneNumber, ResultType.Success, null);
            string token = BuildToken(user);
            return (null, token, user);
        }

        /// <summary>
        /// 邮箱和验证码登录，未注册自动注册
        /// </summary>
        /// <param name="emailAddress">邮箱地址</param>
        /// <param name="secondCode">验证码</param>
        /// <returns></returns>
        public async Task<(LoginFailedResult? LoginFailedResult, string? Token, User? User)>
            LoginByEmailAddressAsync(string emailAddress, string secondCode)
        {
            string key = CacheKey.LoginByEmailAddressPrefix + emailAddress;
            if (!CheckVerifyCode(key, secondCode))
            {
                await PublishLoginEventAsync(null, emailAddress, LoginWay.EmailAddress, ResultType.Fail, LoginFailedResult.EmailVerifcationCodeIsWrong);
                return (LoginFailedResult.EmailVerifcationCodeIsWrong, null, null);
            }
            var user = await userDomainRepository.FindByEmailAdressAsync(emailAddress);
            if (user != null && !user.IsEnable)
            {
                await PublishLoginEventAsync(user.Id, emailAddress, LoginWay.EmailAddress, ResultType.Fail, LoginFailedResult.UserIsForbidden);
                return (LoginFailedResult.UserIsForbidden, null, null);
            }
            user ??= await userDomainRepository.RegisterByEmailAddressAsync(emailAddress);
            await PublishLoginEventAsync(user.Id, emailAddress, LoginWay.EmailAddress, ResultType.Success, null);
            string token = BuildToken(user);
            return (null, token, user);
        }

        public async Task<User> UpdateUserRolesAsync(long userId, long[] roleIds)
        {
            var user = await userDomainRepository.GetUserIncludeRolesAsync(userId);
            if (roleIds.Length == 0)
            {
                user.SetRoles([]);
                return user;
            }
            var roles = await roleDomainRepository.FindByIdsAsync(roleIds);
            user.SetRoles(roles);
            return user;
        }

        public async Task<List<Resource>> GetUserResourcesAsync(long userId)
        {
            if (await userDomainRepository.IsAdminAsync(userId))
                return await resourceDomainRepository.GetAllEnableResourcesAsync();
            else
                return await userDomainRepository.GetUserResourcesAsync(userId);
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

        private async Task PublishLoginEventAsync(long? userId, string loginAccount, LoginWay loginWay, ResultType loginResult, LoginFailedResult? loginFailedResult)
        {
            var msg = "登录成功";
            if (loginFailedResult != null)
                msg = loginFailedResult.GetDescription();
            var ip = clientService.GetClientIpAddress();
            await userDomainRepository.PublishLoginEventAsync(new UserLoginHistory(userId, loginAccount, loginWay, loginResult, ip, msg));
        }
    }
}
