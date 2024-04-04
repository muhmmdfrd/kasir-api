namespace KasirApi.Core.Models.Tools
{
    public class EncryptDecryptDto : BaseRequestTools
    {
        public string Secret { get; set; } = "";
        public int Type { get; set; }
        public string? Guid { get; set; } = null;
    }
}
