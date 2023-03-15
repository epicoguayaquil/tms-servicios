using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly ILogger<UploadFileController> logger;
        private readonly IFileService _fileService;

        public UploadFileController(ILogger<UploadFileController> logger, IFileService fileService)
        {
            this.logger = logger;
            this._fileService = fileService;
        }

        [HttpPost]
        [Route("")]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<ActionResult<GenericResponse>> SubmitPost([FromForm] FileRequest request)
        {
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
            };

            if (request == null)
            {
                return BadRequest(new GenericResponse { Cod = "500", Msg = "Invalid post request" });
            }

            if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))
            {
                return BadRequest(new GenericResponse { Cod = "500", Msg = "Invalid post header" });
            }

            if (request.Image != null)
            {
                response.Data = await _fileService.SaveFIleImageAsync(request);
                return Ok(response);
            }
            else {
                return BadRequest(new GenericResponse { Cod = "404", Msg = "Not updaload Image" });
            }

        }
    }
}
