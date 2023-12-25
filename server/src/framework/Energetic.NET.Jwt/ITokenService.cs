using System.Security.Claims;

namespace Energetic.NET.Jwt
{
    public interface ITokenService
    {
        string BuildToken(IEnumerable<Claim> claims, JwtConfigOptions jwtConfig);

        IEnumerable<Claim> DecodeToken(string token, JwtConfigOptions jwtConfig);
    }
}
