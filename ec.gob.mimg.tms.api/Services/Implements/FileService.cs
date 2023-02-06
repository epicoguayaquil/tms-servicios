using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Helpers;
using ec.gob.mimg.tms.api.Services;
using System.Text;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<FileResponse> SaveFIleImageAsync(FileRequest request)
        {
            FileResponse response = new FileResponse();
            FileModel model = new FileModel();
            model.RUC = request.RUC;
            model.Description = request.Description;

            //...
            
            var uniqueFileName = FileHelper.GetUniqueFileName(request.Image.FileName);
            var uploads = Path.Combine(environment.WebRootPath, "upload", "files", request.RUC.ToString());
            var filePath = Path.Combine(uploads, uniqueFileName);
            //...
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            await request.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));

            //...
            StringBuilder pathFile= new StringBuilder();
            pathFile.Append(String.Format("/upload/files/{0}/{1}", model.RUC.ToString(), uniqueFileName));

            model.Imagepath = pathFile.ToString();
            response.Post = model;
            
            return response;
        }
    }
}
