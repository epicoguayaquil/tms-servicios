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
        private readonly IApiCatastroService _apiCatastroService;
        private readonly IApiMimgService _apiMimgService;
        private readonly IApiActivoMilService _apiActivoMilService;
        private readonly IApiPatenteService _apiPatenteService;

        public ABCTestController(IMapper mapper, TmsDbContext dbContext, 
                                ILogger<ABCTestController> logger,
                                INotificacionService notificacionService,
                                ITokenService tokenService,
                                IApiSriService apiSriService,
                                IApiCatastroService apiCatastroService,
                                IApiMimgService apiMimgService,
                                IApiActivoMilService apiActivoMilService, 
                                IApiPatenteService apiPatenteService)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
            _empresaService = new EmpresaService(_dbContext);
            _establecimientoService = new EstablecimientoService(_dbContext);
            _notificacionService = notificacionService;
            _tokenService = tokenService;
            _apiSriService = apiSriService;
            _apiCatastroService = apiCatastroService;
            _apiMimgService = apiMimgService;
            _apiActivoMilService = apiActivoMilService;
            _apiPatenteService = apiPatenteService;
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


            // Test Mail Tester
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

            //EstablecimientoApiRequest establecimientoApiRequest = new EstablecimientoApiRequest();
            //establecimientoApiRequest.Ruc = "0901986935001";
            //response.Data = await _apiSriService.GetEstablecimientos(establecimientoApiRequest);

            //ActividadApiRequest request = new ActividadApiRequest();
            //request.Ruc = "0901986935001";
            //request.Establecimiento = "2";
            //response.Data = await _apiSriService.GetActividadEstablecimiento(request);

            //response.Data = await _apiSriService.GetEstablecimientosNuevos("2022-12-05");

            PredioApiRequest request = new PredioApiRequest();
            request.IdSector = "90";
            request.Manzana = "1143";
            request.Lote = "19";
            request.Division = "10";
            request.Phv = "0";
            request.Phh = "0";
            request.Numero = "1";
            //...
            response.Data = await _apiCatastroService.GetPredio(request);

            //FactibilidadUsoRequest request = new FactibilidadUsoRequest();
            //request.IdActividad = "1268";
            //request.IdSector = "1";
            //request.Manzana = "6";
            //request.Lote = "1";
            //request.Division = "4";
            //request.Phv = "3";
            //request.Phh = "6";
            //request.Numero = "1";
            ////...
            //response.Data = await _apiMimgService.GetFacilidadUso(request);


            //DimensionesRequest request = new DimensionesRequest();
            //request.IdActividad = "427";
            //request.IdSector = "90";
            //request.Manzana = "1143";
            //request.Lote = "19";
            //request.Division = "0";
            //request.Phv = "0";
            //request.Phh = "0";
            //request.Numero = "1";
            ////...
            //response.Data = await _apiMimgService.GetDimensionMinima(request);


            //ContribuyenteApiRequest request= new ContribuyenteApiRequest();
            //request.Ruc = "0991465502001";
            //response.Data = await _apiActivoMilService.GetByRuc(request);

            //ContribuyenteApiRequest request = new ContribuyenteApiRequest();
            //request.Ruc = "0963036769001";
            //response.Data = await _apiPatenteService.GetByRuc(request);




            return Ok(response);
        }

        
    }
}
