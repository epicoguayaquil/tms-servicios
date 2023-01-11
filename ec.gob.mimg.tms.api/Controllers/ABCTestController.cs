using AutoMapper;
using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using ec.gob.mimg.tms.api.Services;
using ec.gob.mimg.tms.api.Services.Implements;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mail.Services;
using ec.gob.mimg.tms.srv.mail.Services.Implements;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using ec.gob.mimg.tms.srv.mimg.Services;
using ec.gob.mimg.tms.srv.mimg.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ABCTestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TmsDbContext _dbContext;
        private readonly ILogger<ABCTestController> _logger;

        private readonly IEmpresaService _empresaService;
        private readonly INotificacionService _notificacionService;
        private readonly IEstablecimientoService _establecimientoService;

        private readonly ITokenService _tokenService;

        private readonly IApiSriService _apiSriService;

        public ABCTestController(IMapper mapper, TmsDbContext dbContext, 
                                ILogger<ABCTestController> logger,
                                INotificacionService notificacionService,
                                ITokenService tokenService,
                                IApiSriService apiSriService)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
            _empresaService = new EmpresaService(_dbContext);
            _establecimientoService = new EstablecimientoService(_dbContext);
            _notificacionService = notificacionService;
            _tokenService = tokenService;
            _apiSriService = apiSriService;
        }

        // GET: api/Empresas
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            _logger.LogError(">>> Consultando Empresas ..............................");
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
            };


            //// Test Mail Tester
            //NotificacionRequest request = new NotificacionRequest();
            //request.username = "Juan Lafuente";
            //request.mail = "juanklafuente@outlook.com";
            //request.titulo = "Notificación TMS";
            //request.contenido = "Su codigo de seguridad es: 123456";

            //await _notificacionService.EnviarNotificacion(request);

            //// Test Token 
            //TokenRequest tokenRequest = new TokenRequest();
            //await _tokenService.GetToken(tokenRequest);

            //// Test Api SRI 
            //ContribuyenteApiRequest contribuyenteRequest = new ContribuyenteApiRequest();
            //contribuyenteRequest.Ruc = "0901986935001";
            //response.Data = await _apiSriService.GetContribuyente(contribuyenteRequest);

            EstablecimientoApiRequest establecimientoApiRequest = new EstablecimientoApiRequest();
            establecimientoApiRequest.Ruc = "0901986935001";
            response.Data = await _apiSriService.GetEstablecimientos(establecimientoApiRequest);



            ActividadApiRequest request = new ActividadApiRequest();
            request.Ruc = "0901986935001";
            request.Establecimiento = "2";
            response.Data = await _apiSriService.GetActividadEstablecimiento(request);



            return Ok(response);
        }

        
    }
}
