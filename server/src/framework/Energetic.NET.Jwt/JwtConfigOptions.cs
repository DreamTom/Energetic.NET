namespace Energetic.NET.Jwt
{
    public class JwtConfigOptions
    {
        public string SecretKey { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public int Expiry { get; set; }
    }
}