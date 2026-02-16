namespace TaskApp.Infrastructure.Authentication
{
    public class Jwt
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string SecretKey { get; set; } = default!;
        public int ExpiryInDays { get; set; }
    }
}