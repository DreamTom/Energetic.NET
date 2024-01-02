using Energetic.NET.Basic.Domain.IResponsitories;
using Energetic.NET.Basic.Domain.Models;
using Energetic.NET.Jwt;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Energetic.NET.Basic.Domain.Services
{
    public class UserDomainService(IUserDomainRepository userDomainRepository,
            ITokenService tokenService,
            IOptions<JwtConfigOptions> jwtConfigOption)
    {
        public async Task<(bool IsSuccess, string? Token)> LoginByUserNameAndPasswordAsync(string userName, string password)
        {
            var user = await userDomainRepository.FindByUserNameAsync(userName);
            if (user == null)
                return (false, null);
            if (!user.CheckPassword(password))
                return (false, null);
            string token = BuildToken(user);
            return (true, token);
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
