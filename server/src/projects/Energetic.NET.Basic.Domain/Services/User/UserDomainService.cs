using EasyCaching.Core;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IResponsitories;
using Energetic.NET.Basic.Domain.Models;
using Energetic.NET.Jwt;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;

namespace Energetic.NET.Basic.Domain.Services
{
    public class UserDomainService(IUserDomainRepository userDomainRepository,
            ITokenService tokenService,
            IEasyCachingProvider cachingProvider,
            IOptions<JwtConfigOptions> jwtConfigOption)
    {
        public async Task<(bool IsSuccess, string? Token, User? User)> LoginByUserNameAndPasswordAsync(string userName, string password)
        {
            var user = await userDomainRepository.FindByUserNameAsync(userName);
            if (user == null || !user.CheckPassword(password))
                return (false, null, null);
            string token = BuildToken(user);
            return (true, token, user);
        }

        public async Task<(bool IsSuccess, string? Token, User? User)> LoginByPhoneNumberAsync(string phoneNumber, string secondCode)
        {
            string key = $"Login_By_PhoneNumber_{phoneNumber}";
            if (!CheckVerifyCode(key, secondCode))
                return (false, null, null);
            var user = await userDomainRepository.FindByPhoneNumberAsync(phoneNumber);
            user ??= await userDomainRepository.RegisterByPhoneNumberAsync(phoneNumber);
            string token = BuildToken(user);
            return (true, token, user);
        }

        public async Task<(bool IsSuccess, string? Token, User? User)> LoginByEmailAddressAsync(string emailAddress, string secondCode)
        {
            string key = $"Login_By_EmailAddress_{emailAddress}";
            if (!CheckVerifyCode(key, secondCode))
                return (false, null, null);
            var user = await userDomainRepository.FindByEmailAdressAsync(emailAddress);
            user ??= await userDomainRepository.RegisterByEmailAddressAsync(secondCode);
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
                new(JwtRegisteredClaimNames.Name, user.NickName),
            };
            if (!string.IsNullOrWhiteSpace(user.UserName))
                claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
            return tokenService.BuildToken(claims, jwtConfigOption.Value);
        }
    }
}
