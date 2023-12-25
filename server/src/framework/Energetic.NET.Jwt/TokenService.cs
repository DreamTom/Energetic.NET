using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Jwt
{
    public class TokenService : ITokenService
    {
        public string BuildToken(IEnumerable<Claim> claims, JwtConfigOptions jwtConfig)
        {
            ArgumentNullException.ThrowIfNull(jwtConfig);

            var expiryDuration = TimeSpan.FromSeconds(jwtConfig.Expiry);
            byte[] secKeyBytes = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);
            var securityKey = new SymmetricSecurityKey(secKeyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                   issuer: jwtConfig.Issuer,
                   audience: jwtConfig.Audience,
                   expires: DateTime.Now.Add(expiryDuration),
                   claims: claims,
                   signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public IEnumerable<Claim> DecodeToken(string token, JwtConfigOptions jwtConfig)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] secKeyBytes = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);
            var securityKey = new SymmetricSecurityKey(secKeyBytes);
            TokenValidationParameters valParam = new()
            {
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtConfig.Audience,
                ValidIssuer = jwtConfig.Issuer,
                ValidateLifetime = true,
                RequireExpirationTime = true,
            };
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, valParam, out _);
            var claims = claimsPrincipal.Claims;
            long expires = long.Parse(claims.Single(c => c.Type == JwtRegisteredClaimNames.Exp).Value);
            if (expires < DateTimeOffset.Now.ToUnixTimeSeconds())
                throw new SecurityTokenExpiredException();
            return claims;
        }
    }
}
