using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IFileService
    {
        Task<FileResponse> SaveFIleImageAsync(FileRequest postRequest);
    }
}