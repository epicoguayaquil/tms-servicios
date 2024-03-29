﻿using AutoMapper;
using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using ec.gob.mimg.tms.api.Services;
using ec.gob.mimg.tms.api.Services.Implements;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mail.Services;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using ec.gob.mimg.tms.srv.mimg.Models;
using ec.gob.mimg.tms.srv.mimg.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TmsDbContext _dbContext;
        private readonly ILogger<EmpresaController> _logger;

        private readonly IEmpresaService _empresaService;
        private readonly INotificacionService _notificacionService;
        private readonly IEstablecimientoService _establecimientoService;
        private readonly IEmpresaObligacionService _empresaObligacionService;
        private readonly IObligacionService _obligacionService;
        private readonly IObligacionCaracteristicaService _obligacionCaracteristicaService;
        private readonly IRegistroNotificacionService _registroNotificacionService;
        private readonly IApiSriService _apiSriService;
        private readonly INotificacionMotivoFormatoService _notificacionMotivoFormatoService;

        public EmpresaController(IMapper mapper, TmsDbContext dbContext,
                                ILogger<EmpresaController> logger,
                                INotificacionService notificacionService,
                                IApiSriService apiSriService)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
            _empresaService = new EmpresaService(_dbContext);
            _establecimientoService = new EstablecimientoService(_dbContext);
            _notificacionService = notificacionService;
            _empresaObligacionService = new EmpresaObligacionService(_dbContext);
            _obligacionService = new ObligacionService(_dbContext);
            _obligacionCaracteristicaService = new ObligacionCaracteristicaService(_dbContext);
            _registroNotificacionService = new RegistroNotificacionService(_dbContext);
            _apiSriService = apiSriService;
            _notificacionMotivoFormatoService = new NotificacionMotivoFormatoService(_dbContext);
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

            var empresaList = await _empresaService.GetAllAsync();
            response.Data = empresaList.Select(x => _mapper.Map<EmpresaResponse>(x));

            return Ok(response);
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var empresa = await _empresaService.GetById(id);

            if (empresa == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EmpresaResponse>(empresa)
                };
                return Ok(response);
            }
        }

        // GET: api/Empresas/byRuc/5
        [HttpGet("byRuc/{ruc}")]
        public async Task<ActionResult<GenericResponse>> GetByRuc(string ruc)
        {
            var empresa = await _empresaService.GetByRucAsync(ruc);

            if (empresa == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EmpresaResponse>(empresa)
                };
                return Ok(response);
            }
        }

        // GET: api/Empresas/5/establecimientos
        [HttpGet("{id}/establecimientos")]
        public async Task<ActionResult<GenericResponse>> GetEstablecimientosById(int id)
        {
            var establecimientoList = await _establecimientoService.GetAsync(x => x.EmpresaId == id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = establecimientoList.Select(x => _mapper.Map<EstablecimientoResponse>(x))
            };
            return Ok(response);
        }

        // POST: api/Empresas/cargarNuevosEstablecimientos/2022-12-05
        [HttpPost("cargarNuevosEstablecimientos/{fechaBusqueda}")]
        public async Task<ActionResult<GenericResponse>> CargarNuevosEstablecimientos(string fechaBusqueda)
        {
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK"
            };
            EstablecimientoApiResponse establecimientoApiResponse = await _apiSriService.GetEstablecimientosNuevos(fechaBusqueda);
            ResultadoModel resultadoApi = establecimientoApiResponse.Resultado;
            if (!resultadoApi.Ok)
            {
                response = new()
                {
                    Cod = resultadoApi.StatusCode.ToString(),
                    Msg = resultadoApi.Titulo,
                    Data = resultadoApi
                };
                return Ok(response);
            }
            List<EstablecimientoModel> establecimientoModelList = establecimientoApiResponse.DataResult;
            List<EstablecimientoModel> establecimientoModelListOpen = new();
            foreach (EstablecimientoModel establecimientoModel in establecimientoModelList)
            {
                if (establecimientoModel.Estado == EstadoEstablecimientoEnum.ABIERTO.ToString())
                {
                    var empresa = await _empresaService.GetByRucAsync(establecimientoModel.Ruc);
                    if (empresa == null)
                    {
                        ContribuyenteApiRequest contribuyenteRequest = new()
                        {
                            Ruc = establecimientoModel.Ruc
                        };
                        ContribuyenteApiResponse contribuyenteApiResponse = await _apiSriService.GetContribuyente(contribuyenteRequest);
                        if (contribuyenteApiResponse.Resultado.Ok)
                        {
                            ContribuyenteModel contribuyenteModel = contribuyenteApiResponse.DataResult;
                            empresa = new() {
                                Ruc = contribuyenteModel.Ruc,
                                NombreComercial = contribuyenteModel.RazonSocial,
                                Direccion = contribuyenteModel.Email,
                                Telefono = contribuyenteModel.Telefono,
                                FechaRegistro = DateTime.Now,
                                UsuarioRegistro = "admin@mail.com",
                                Estado = EstadoEnum.ACTIVO.ToString()
                            };
                            bool isSaved = await _empresaService.AddAsync(empresa);
                            if (isSaved)
                            {
                                TmsEstablecimiento establecimiento = new() {
                                    EmpresaId = empresa.IdEmpresa,
                                    NumeroEstablecimiento = establecimientoModel.Establecimiento,
                                    Direccion = establecimientoModel.Direccion,
                                    Email = contribuyenteModel.Email,
                                    SrifechaRegistro = DateTime.Now,
                                    FechaRegistro = DateTime.Now,
                                    UsuarioRegistro = "admin@mail.com",
                                    Estado = EstadoEstablecimientoEnum.INHABILITADO.ToString(),
                                    EstadoRegistro = EstadoRegistroEnum.NO_REGISTRADO.ToString()
                                };

                                bool isSavedEstablecimeinto = await _establecimientoService.AddAsync(establecimiento);

                                if (isSavedEstablecimeinto)
                                {
                                    //TODO: Enviar notificacion
                                    await EnviarNotificacionAsync(empresa, establecimiento, MotivoNotificacionEnum.NUEVO_ESTABLECIMIENTO);
                                }
                            }
                        }
                    }
                    else
                    {
                        var establecimiento = await _establecimientoService.GetByEmpresaIdAndNumero(empresa.IdEmpresa, establecimientoModel.Establecimiento);
                        if (establecimiento == null)
                        {
                            establecimiento = new()
                            {
                                EmpresaId = empresa.IdEmpresa,
                                NumeroEstablecimiento = establecimientoModel.Establecimiento,
                                Direccion = establecimientoModel.Direccion,
                                Email = empresa.Direccion,
                                SrifechaRegistro = DateTime.Now,
                                FechaRegistro = DateTime.Now,
                                UsuarioRegistro = "admin@mail.com",
                                Estado = EstadoEstablecimientoEnum.INHABILITADO.ToString(),
                                EstadoRegistro = EstadoRegistroEnum.NO_REGISTRADO.ToString()
                            };

                            bool isSavedEstablecimeinto = await _establecimientoService.AddAsync(establecimiento);

                            if (isSavedEstablecimeinto)
                            {
                                //TODO: Enviar notificacion
                                await EnviarNotificacionAsync(empresa, establecimiento, MotivoNotificacionEnum.NUEVO_ESTABLECIMIENTO);
                            }
                        }
                    }
                    establecimientoModelListOpen.Add(establecimientoModel);
                }
            }
            response.Data = establecimientoModelListOpen;
            return Ok(response);
        }

        private async Task EnviarNotificacionAsync(TmsEmpresa empresa, TmsEstablecimiento establecimiento, MotivoNotificacionEnum motivoEnum)
        {
            var notificacionMotivo = await _notificacionMotivoFormatoService.GetByMotivo(motivoEnum.ToString());
            if (notificacionMotivo == null)
            {
                return;
            }
            string cuerpoNotificacion = notificacionMotivo.Cuerpo;
            cuerpoNotificacion = cuerpoNotificacion.Replace("+razon_social+", empresa.NombreComercial);
            cuerpoNotificacion = cuerpoNotificacion.Replace("+numero_establecimiento+", establecimiento.NumeroEstablecimiento);
            string mail = establecimiento.Email == null ? "" : establecimiento.Email;
            TmsNotificacion notificacion = new()
            {
                FechaEnvio = DateTime.Now,
                Jerarquia = JerarquiaNotificacion.ESTABLECIMIENTO.ToString(),
                JerarquiaObjetoId = establecimiento.IdEstablecimiento,
                Motivo = motivoEnum.ToString(),
                Titulo = notificacionMotivo.Titulo,
                Cuerpo = cuerpoNotificacion,
                Destinatarios = mail,
                Leido = "NO",
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = "admin@mail.com",
                Estado = EstadoEnum.ACTIVO.ToString()
            };
            bool isSaved = await _registroNotificacionService.AddAsync(notificacion);
            if (isSaved)
            {
                NotificacionRequest request = new NotificacionRequest
                {
                    username = empresa.NombreComercial,
                    mail = notificacion.Destinatarios,
                    titulo = notificacion.Titulo,
                    contenido = notificacion.Cuerpo
                };

                await _notificacionService.EnviarNotificacion(request);
            }
        }

        // POST: api/Empresas
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(EmpresaRequest empresaRequest)
        {
            try
            {
                TmsEmpresa empresa = new();
                empresa = _mapper.Map<TmsEmpresa>(empresaRequest);
                empresa.FechaRegistro = DateTime.Now;
                empresa.UsuarioRegistro = "admin@mail.com";
                empresa.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _empresaService.AddAsync(empresa);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EmpresaResponse>(empresa)
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        // DELETE: api/TmsEmpresas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _empresaService.GetById(id);

            if (empresa == null)
            {
                return NotFound();
            }

            await _empresaService.DeleteAsync(empresa);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/TmsEmpresas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Update(EmpresaRequest empresaRequest)
        {
            try
            {
                var empresaActual = await _empresaService.GetByRucAsync(empresaRequest.Ruc);

                if (empresaActual == null) { return NotFound(); }

                TmsEmpresa empresa = _mapper.Map<TmsEmpresa>(empresaRequest);
                empresa.IdEmpresa = empresaActual.IdEmpresa;

                empresa.FechaRegistro = empresaActual.FechaRegistro;
                empresa.UsuarioRegistro = empresaActual.UsuarioRegistro;
                empresa.Estado = empresaActual.Estado;

                empresa.FechaModificacion = DateTime.Now;
                empresa.UsuarioModificacion = "admin@mail.com";


                bool isSaved = await _empresaService.UpdateAsync(empresa);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EmpresaResponse>(empresa)
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        // GET: api/Empresa/1/obligaciones
        [HttpGet("{id}/obligaciones")]
        public async Task<ActionResult<GenericResponse>> GetAllObligacionesById(int id)
        {
            var empresaObligacionList = await _empresaObligacionService.GetListByEmpresaId(id);
            var empresaObligacionResponseListNew = new List<ObjetoObligacionResponse>();
            foreach (var empresaObligacion in empresaObligacionList)
            {
                var empresaObligacionResponse = _mapper.Map<ObjetoObligacionResponse>(empresaObligacion);
                empresaObligacionResponse.IdObjetoObligacion = empresaObligacion.IdEmpresaObligacion;
                empresaObligacionResponse.ReferenciaId = empresaObligacion.EmpresaId;
                var obligacion = await _obligacionService.GetById(empresaObligacion.ObligacionId);
                empresaObligacionResponse.NombreObligacion = obligacion.Nombre;
                empresaObligacionResponse.OrdenEjecucion = obligacion.OrdenEjecucion;

                var obligacionCaracteristicaList = await _obligacionCaracteristicaService.GetListByObligacionIdAndTipo(empresaObligacion.ObligacionId, TipoCaracteristica.GESTION.ToString());
                ProcesarCaracteristicasDeGestion(obligacionCaracteristicaList, empresaObligacionResponse, empresaObligacion.IdEmpresaObligacion);

                empresaObligacionResponseListNew.Add(empresaObligacionResponse);
            }
            _obligacionService.ProcesarVariablesDeObligaciones(empresaObligacionResponseListNew);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = empresaObligacionResponseListNew
            };

            return Ok(response);
        }

        private void ProcesarCaracteristicasDeGestion(ICollection<TmsObligacionCaracteristica>? obligacionCaracteristicaList,
            ObjetoObligacionResponse empresaObligacionRequest, int idEmpresaObligacion)
        {
            if (obligacionCaracteristicaList == null)
            {
                return;
            }
            foreach (var obligacionCaracteristica in obligacionCaracteristicaList)
            {
                if (obligacionCaracteristica.Nombre == "tipo_generacion")
                {
                    empresaObligacionRequest.TipoGeneracion = obligacionCaracteristica.Valor;
                }
                else if (obligacionCaracteristica.Nombre == "tiene_formulario")
                {
                    empresaObligacionRequest.TieneFormulario = (obligacionCaracteristica.Valor == "Si");
                }
                else if (obligacionCaracteristica.Nombre == "permite_excepción")
                {
                    empresaObligacionRequest.PermiteExcepcion = (obligacionCaracteristica.Valor == "Si");
                }
                else if (obligacionCaracteristica.Nombre == "URL_excepcion")
                {
                    if (obligacionCaracteristica.Valor == null)
                    {
                        empresaObligacionRequest.UrlExcepcion = "";
                    }
                    else
                    {
                        empresaObligacionRequest.UrlExcepcion = obligacionCaracteristica.Valor.Replace("{id}", idEmpresaObligacion.ToString());
                    }
                }
                else if (obligacionCaracteristica.Nombre == "URL_ejecucion")
                {
                    if (obligacionCaracteristica.Valor == null)
                    {
                        empresaObligacionRequest.UrlEjecucion = "";
                    }
                    else
                    {
                        empresaObligacionRequest.UrlEjecucion = obligacionCaracteristica.Valor.Replace("{id}", idEmpresaObligacion.ToString());
                    }
                }
            }
        }

        // POST: api/Empresa/1/obligacionesGenerales
        [HttpPost("{id}/obligacionesGenerales")]
        public async Task<ActionResult<GenericResponse>> GenerarObligacionesGenerales(int id)
        {
            var obligacionesList = await _obligacionService.GetListByJerarquiaAndEstado(JerarquiaObligacion.EMPRESA.ToString(),
                EstadoEnum.ACTIVO.ToString());
            int contadorGeneradas = 0;
            foreach (var obligacion in obligacionesList)
            {
                TmsEmpresaObligacion empresaObligacion;
                empresaObligacion = await _empresaObligacionService.GetByEmpresaIdAndObligacionId(id, obligacion.IdObligacion);
                if (empresaObligacion == null)
                {
                    empresaObligacion = new()
                    {
                        EmpresaId = id,
                        ObligacionId = obligacion.IdObligacion,
                        Observacion = "",
                        FechaExigibilidad = DateTime.Now,
                        FechaRenovacion = DateTime.Now,
                        Estado = EstadoObligacionEnum.NO_CUMPLE.ToString(),
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "admin@mail.com"
                    };
                    bool isSaved = await _empresaObligacionService.AddAsync(empresaObligacion);
                    if (isSaved)
                    {
                        contadorGeneradas++;
                    }
                }
            }
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Obligaciones generadas: " + contadorGeneradas
            };

            return Ok(response);
        }

        // GET: api/Empresas/5/notificaciones
        [HttpGet("{id}/notificaciones")]
        public async Task<ActionResult<GenericResponse>> GetNotificacionesById(int id)
        {
            var notificacionList = await _registroNotificacionService.GetListByJerarquiaAndObjetoId("Empresa", id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = notificacionList.Select(x => _mapper.Map<RegistroNotificacionResponse>(x))
            };
            return Ok(response);
        }

    }
}
