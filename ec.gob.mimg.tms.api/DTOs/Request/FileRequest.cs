using System.Text.Json.Serialization;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FileRequest
    {
        public string RUC { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }

    }
}