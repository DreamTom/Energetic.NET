namespace Energetic.NET.Jwt
{
    public record JwtConfigOptions(string SecretKey, string Issuer, string Audience, int Expiry)
    {
    }
}