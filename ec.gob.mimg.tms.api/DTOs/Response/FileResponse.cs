namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class FileResponse
    {
        public FileModel Post { get; set; }
    }

    public class FileModel
    {
        public string RUC { get; set; }
        public string Description { get; set; }
        public string Imagepath { get; set; }
    }
}
