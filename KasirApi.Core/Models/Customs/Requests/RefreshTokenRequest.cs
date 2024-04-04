namespace KasirApi.Core.Models.Customs.Requests
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = "";
        public string? Username { get; set; }

    }
}
