namespace KasirApi.Core.Configs
{
    public class JwtConfigs
    {
        public string TokenSecret { get; set; } = "";
        public string PasswordSecret { get; set; } = "";
        public int TokenLifeTimes { get; set; }
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
    }
}
