using Microsoft.AspNetCore.Http;

namespace CVBanken.Data.Models.Requests
{
    public class UploadFileRequest
    {
        public IFormFile[] FileData { get; set; }
        public int? CvIndex { get; set; }
        public string? Owner { get; set; } //for admin
    }
}