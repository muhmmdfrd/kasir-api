namespace KasirApi.Core.Models.Customs.Requests
{
    public class UploadRequest
    {
        public string WebRootPath { get; set; } = "wwwroot";
        public string BaseUrl { get; set; } = "";
        public string Base64 { get; set; } = "";
        public string Folder { get; set; } = "";
        public string FileName { get; set; } = "";
        public long CreatedBy { get; set; }
        public long UserId { get; set; }

    }
}
