namespace KasirApi.Core.Models.Customs.Requests
{
    public class PasswordlessRequest
    {
        public string OTP { get; set; } = "";
        public string Username { get; set; } = "";
    }
}
