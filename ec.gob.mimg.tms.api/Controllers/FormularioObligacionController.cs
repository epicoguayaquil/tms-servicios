using Microsoft.AspNetCore.Mvc;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Services.Implements;
using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.Services;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using Microsoft.IdentityModel.Tokens;
using ec.gob.mimg.tms.srv.mimg.Services;
using ec.gob.mimg.tms.api.Enums;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioObligacionController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IFormularioObligacionService _formularioObligacionService;
        private readonly IFormularioObligacionCaracteristicaValorService _formularioObligacionCaracteristicaValorService;
        private readonly IObligacionService _obligacionService;
        private readonly IFormularioDetalleService _formularioDetalleService;
        private readonly IFormularioActividadService _formularioActividadService;
        private readonly IApiMimgService _apiMimgService;
        private readonly IEmpresaService _empresaService;
        private readonly IEstablecimientoService _establecimientoService;
        private readonly IFormularioService _formularioService;
        private readonly IObligacionCaracteristicaService _obligacionCaracteristicaService;

        private readonly IMapper _mapper;

        public FormularioObligacionController(IMapper mapper, TmsDbContext dbContext, IApiMimgService apiMimgService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _formularioObligacionService = new FormularioObligacionService(_dbContext);
            _formularioObligacionCaracteristicaValorService = new FormularioObligacionCaracteristicaValorService(_dbContext);
            _obligacionService = new ObligacionService(_dbContext);
            _formularioDetalleService = new FormularioDetalleService(_dbContext);
            _formularioActividadService = new FormularioActividadService(_dbContext);
            _apiMimgService = apiMimgService;
            _empresaService = new EmpresaService(_dbContext);
            _establecimientoService = new EstablecimientoService(_dbContext);
            _formularioService = new FormularioService(_dbContext);
            _obligacionCaracteristicaService = new ObligacionCaracteristicaService(_dbContext);
        }

        // GET: api/FormularioObligacion
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var formularioObligacionList = await _formularioObligacionService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioObligacionList.Select(x => _mapper.Map<FormularioObligacionResponse>(x))
            };

            return Ok(response);
        }

        // POST: api/FormularioObligacion/porFiltro
        [HttpPost("porFiltro")]
        public async Task<ActionResult<GenericResponse>> GetByFilter(FormularioObligacionFiltroRequest filtro)
        {
            ICollection<TmsFormulario> formularioList;
            GenericResponse responseFail;
            if (filtro.RUC == null || filtro.RUC.IsNullOrEmpty())
            {
                formularioList = await _formularioService.GetListByEstado(EstadoEnum.ACTIVO.ToString());
            }
            else
            {
                formularioList = new List<TmsFormulario>();
                var empresa = await _empresaService.GetByRucAsync(filtro.RUC);
                if (empresa == null)
                {
                    responseFail = new()
                    {
                        Cod = "400",
                        Msg = "FAIL",
                        Data = "Contribuyente no encontrado"
                    };

                    return Ok(responseFail);
                }
                var establecimientoList = await _establecimientoService.GetAsync(x => x.EmpresaId == empresa.IdEmpresa);
                if (establecimientoList == null || establecimientoList.IsNullOrEmpty())
                {
                    responseFail = new()
                    {
                        Cod = "400",
                        Msg = "FAIL",
                        Data = "Contribuyente no tiene establecimientos"
                    };
                    return Ok(responseFail);
                }
                foreach (TmsEstablecimiento establecimiento in establecimientoList)
                {
                    var formularioActivoList = await _formularioService.GetListByEstablecimientoIdAndEstado(
                        establecimiento.IdEstablecimiento, EstadoEnum.ACTIVO.ToString());
                    if (formularioActivoList != null && !formularioActivoList.IsNullOrEmpty())
                    {
                        var formulario = formularioActivoList.First();
                        if (filtro.EstablecimientoId == null)
                        {
                            formularioList.Add(formulario);
                        }
                        else
                        {
                            if (filtro.EstablecimientoId == establecimiento.IdEstablecimiento)
                            {
                                formularioList.Add(formulario);
                            }
                        }
                    }
                }
            }
            if (formularioList.IsNullOrEmpty())
            {
                responseFail = new()
                {
                    Cod = "400",
                    Msg = "FAIL",
                    Data = "No se encontraron formularios activos en el(los) establecimiento(s)"
                };
                return Ok(responseFail);
            }
            var formularioObligacionResponseList = new List<FormularioObligacionResponse>();
            foreach (var formulario in formularioList) {
                var establecimiento = await _establecimientoService.GetById(formulario.EstablecimientoId);
                var empresa = await _empresaService.GetById(establecimiento.EmpresaId);
                var formularioObligacionList = await _formularioObligacionService.GetListByFormularioId(formulario.IdFormulario);
                foreach (var formularioObligacion in formularioObligacionList)
                {
                    var formularioObligacionRequest = _mapper.Map<FormularioObligacionResponse>(formularioObligacion);
                    var obligacion = await _obligacionService.GetById(formularioObligacion.ObligacionId);
                    formularioObligacionRequest.NombreObligacion = obligacion.Nombre;
                    formularioObligacionRequest.OrdenEjecucion = obligacion.OrdenEjecucion;
                    formularioObligacionRequest.EmpresaRuc = empresa.Ruc;
                    formularioObligacionRequest.EmpresaNombre = empresa.NombreComercial;
                    formularioObligacionRequest.EstablecimientoNumero = establecimiento.NumeroEstablecimiento;
                    formularioObligacionRequest.EstablecimientoNombre = establecimiento.NombreComercial;

                    var obligacionCaracteristicaList = await _obligacionCaracteristicaService.GetListByObligacionIdAndTipo(formularioObligacion.ObligacionId, TipoCaracteristica.GESTION.ToString());
                    ProcesarCaracteristicasDeGestion(obligacionCaracteristicaList, formularioObligacionRequest, formularioObligacion.IdFormularioObligacion);
                    if (formularioObligacionRequest.TipoGeneracion == filtro.TipoGeneracion
                        && formularioObligacionRequest.Estado == filtro.Estado)
                    {
                        if (filtro.Anio == null || filtro.Anio <= 0)
                        {
                            formularioObligacionResponseList.Add(formularioObligacionRequest);
                        }
                        else
                        {
                            if (formularioObligacionRequest.FechaRegistro.Year == filtro.Anio)
                            {
                                formularioObligacionResponseList.Add(formularioObligacionRequest);
                            }
                        }
                    }
                }
            }
            if (formularioObligacionResponseList.IsNullOrEmpty())
            {
                responseFail = new()
                {
                    Cod = "400",
                    Msg = "FAIL",
                    Data = "Contribuyente no posee obligaciones pendientes"
                };
                return Ok(responseFail);
            }

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioObligacionResponseList.Select(x => _mapper.Map<FormularioObligacionResponse>(x))
            };

            return Ok(response);
        }

        private static void ProcesarCaracteristicasDeGestion(ICollection<TmsObligacionCaracteristica>? obligacionCaracteristicaList,
                FormularioObligacionResponse formularioObligacionRequest, int idFormularioObligacion)
        {
            if (obligacionCaracteristicaList == null)
            {
                return;
            }
            foreach (var obligacionCaracteristica in obligacionCaracteristicaList)
            {
                if (obligacionCaracteristica.Nombre == "tipo_generacion")
                {
                    formularioObligacionRequest.TipoGeneracion = obligacionCaracteristica.Valor;
                }
                else if (obligacionCaracteristica.Nombre == "tiene_formulario")
                {
                    formularioObligacionRequest.TieneFormulario = (obligacionCaracteristica.Valor == "Si");
                }
                else if (obligacionCaracteristica.Nombre == "permite_excepción")
                {
                    formularioObligacionRequest.PermiteExcepcion = (obligacionCaracteristica.Valor == "Si");
                }
                else if (obligacionCaracteristica.Nombre == "URL_excepcion")
                {
                    if (obligacionCaracteristica.Valor == null)
                    {
                        formularioObligacionRequest.UrlExcepcion = "";
                    }
                    else
                    {
                        formularioObligacionRequest.UrlExcepcion = obligacionCaracteristica.Valor.Replace("{id}", idFormularioObligacion.ToString());
                    }
                }
                else if (obligacionCaracteristica.Nombre == "URL_ejecucion")
                {
                    if (obligacionCaracteristica.Valor == null)
                    {
                        formularioObligacionRequest.UrlEjecucion = "";
                    }
                    else
                    {
                        formularioObligacionRequest.UrlEjecucion = obligacionCaracteristica.Valor.Replace("{id}", idFormularioObligacion.ToString());
                    }
                }
            }
        }

        // GET: api/FormularioObligacion/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formularioObligacion = await _formularioObligacionService.GetById(id);

            if (formularioObligacion == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<FormularioObligacionResponse>(formularioObligacion)
                };
                return Ok(response);
            }
        }

        // POST: api/FormularioObligacion
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioObligacionRequest formularioObligacionRequest)
        {
            try
            {
                TmsFormularioObligacion formularioObligacion = new TmsFormularioObligacion();
                formularioObligacion = _mapper.Map<TmsFormularioObligacion>(formularioObligacionRequest);
                formularioObligacion.FechaRegistro = DateTime.Now;
                formularioObligacion.UsuarioRegistro = "admin@mail.com";
                //formularioObligacion.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioObligacionService.AddAsync(formularioObligacion);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionResponse>(formularioObligacion)
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

        // DELETE: api/FormularioObligacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formularioObligacion = await _formularioObligacionService.GetById(id);

            if (formularioObligacion == null)
            {
                return NotFound();
            }

            await _formularioObligacionService.DeleteAsync(formularioObligacion);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/FormularioObligacion/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FormularioObligacionRequest formularioObligacionRequest)
        {
            try
            {
                var formularioObligacionActual = await _formularioObligacionService.GetById(id);
                if (formularioObligacionActual == null) { return NotFound(); }

                formularioObligacionActual.ObligacionId = formularioObligacionRequest.ObligacionId;
                formularioObligacionActual.FormularioId = formularioObligacionRequest.FormularioId;
                formularioObligacionActual.Observacion = formularioObligacionRequest.Observacion;
                formularioObligacionActual.FechaExigibilidad = formularioObligacionRequest.FechaExigibilidad;
                formularioObligacionActual.FechaRenovacion = formularioObligacionRequest.FechaRenovacion;
                formularioObligacionActual.FechaModificacion = DateTime.Now;
                formularioObligacionActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _formularioObligacionService.UpdateAsync(formularioObligacionActual);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionResponse>(formularioObligacionActual)
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

        // PUT: api/FormularioObligacion/1/gestionar
        [HttpPut("{id}/gestionar")]
        public async Task<IActionResult> Gestionar(int id, FormularioObligacionGestionarRequest formularioObligacionRequest)
        {
            try
            {
                var formularioObligacionActual = await _formularioObligacionService.GetById(id);
                if (formularioObligacionActual == null) { return NotFound(); }

                formularioObligacionActual.Observacion = formularioObligacionRequest.Observacion;
                formularioObligacionActual.Estado = formularioObligacionRequest.Estado;
                formularioObligacionActual.FechaModificacion = DateTime.Now;
                formularioObligacionActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _formularioObligacionService.UpdateAsync(formularioObligacionActual);

                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionResponse>(formularioObligacionActual)
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

        // GET: api/FormularioObligacion/1/caracteristicas
        [HttpGet("{id}/caracteristicas")]
        public async Task<ActionResult<GenericResponse>> GetAllCaracteristicasById(int id)
        {
            var formularioObligacionCaracteristicaValorList = await _formularioObligacionCaracteristicaValorService.GetListByFormularioObligacionId(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioObligacionCaracteristicaValorList.Select(x => _mapper.Map<FormularioObligacionCaracteristicaValorResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/FormularioObligacion/1/ejecutar
        [HttpPost("{id}/ejecutar")]
        public async Task<ActionResult<GenericResponse>> EjecutarValidacion(int id)
        {
            var formularioObligacion = await _formularioObligacionService.GetById(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK"
            };

            if (formularioObligacion.Estado == EstadoObligacionEnum.CUMPLE.ToString())
            {
                response.Data = "Obligacion ya cumple";
                return Ok(response);
            }
            var obligacion = await _obligacionService.GetById(formularioObligacion.ObligacionId);

            if (obligacion.Nombre == "Uso de Suelo")
            {
                var formularioDetalleList = await _formularioDetalleService.GetListByFormularioIdAndPasoCreacion(obligacion.IdObligacion, 2);
                if (formularioDetalleList.IsNullOrEmpty())
                {
                    return BadRequest();
                }
                FactibilidadUsoRequest request = GenerarRequestUsoSuelo(formularioDetalleList);

                var formularioActividadList = await _formularioActividadService.GetListByFormularioId(formularioObligacion.FormularioId);
                TmsFormularioActividad formularioActividad = formularioActividadList.First();
                request.IdActividad = formularioActividad.ActividadEconomicaId.ToString();

                var responseUsoSuelo = await _apiMimgService.GetFacilidadUso(request);

                formularioObligacion.FechaModificacion = DateTime.Now;
                formularioObligacion.UsuarioModificacion = "admin@mail.com";

                if (!responseUsoSuelo.DataResult)
                {
                    formularioObligacion.Observacion = responseUsoSuelo.Resultado.Titulo;
                    if (responseUsoSuelo.Resultado.StatusCode == 200)
                    {
                        formularioObligacion.Estado = EstadoObligacionEnum.EN_EXCEPCION.ToString();
                    }
                    else
                    {
                        formularioObligacion.Estado = EstadoObligacionEnum.NO_CUMPLE.ToString();
                    }

                    response.Cod = "500";
                    response.Msg = "FAIL";
                    response.Data = responseUsoSuelo.Resultado;
                }
                else
                {
                    formularioObligacion.Estado = EstadoObligacionEnum.CUMPLE.ToString();
                    response.Data = "Cumplió";
                }
                bool isUpdate = await _formularioObligacionService.UpdateAsync(formularioObligacion);

                if (!isUpdate)
                {
                    response.Msg += " No se actualizo el registro";
                }
            }
            else if (obligacion.Nombre == "Dimensiones Mínimas")
            {
                var formularioDetalleList = await _formularioDetalleService.GetListByFormularioIdAndPasoCreacion(obligacion.IdObligacion, 2);
                if (formularioDetalleList.IsNullOrEmpty())
                {
                    return BadRequest();
                }
                DimensionesRequest request = GenerarRequestDimensiones(formularioDetalleList);

                var formularioActividadList = await _formularioActividadService.GetListByFormularioId(formularioObligacion.FormularioId);
                TmsFormularioActividad formularioActividad = formularioActividadList.First();
                request.IdActividad = formularioActividad.ActividadEconomicaId.ToString();
                var responseDimensiones = await _apiMimgService.GetDimensionMinima(request);
                
                formularioObligacion.FechaModificacion = DateTime.Now;
                formularioObligacion.UsuarioModificacion = "admin@mail.com";                
                if (responseDimensiones.DataResult == null)
                {
                    formularioObligacion.Observacion = responseDimensiones.Resultado.Titulo;
                    if (responseDimensiones.Resultado.StatusCode == 200)
                    {
                        formularioObligacion.Estado = EstadoObligacionEnum.EN_EXCEPCION.ToString();
                    }
                    else
                    {
                        formularioObligacion.Estado = EstadoObligacionEnum.NO_CUMPLE.ToString();
                    }
                    response.Cod = "500";
                    response.Msg = "FAIL";
                    response.Data = responseDimensiones.Resultado;
                }
                else
                {
                    formularioObligacion.Estado = EstadoObligacionEnum.CUMPLE.ToString();
                    response.Data = "Cumplió";
                }
                bool isUpdate = await _formularioObligacionService.UpdateAsync(formularioObligacion);

                if (!isUpdate)
                {
                    response.Msg += " No se actualizo el registro";
                }
                
            }
            return Ok(response);
        }

        private FactibilidadUsoRequest GenerarRequestUsoSuelo(ICollection<TmsFormularioDetalle> formularioDetalleList)
        {
            FactibilidadUsoRequest request = new();

            foreach (var detalleElement in formularioDetalleList)
            {
                if (String.Equals(detalleElement.Caracteristica, "Sector", StringComparison.OrdinalIgnoreCase))
                {
                    request.IdSector = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Manzana", StringComparison.OrdinalIgnoreCase))
                {
                    request.Manzana = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Lote", StringComparison.OrdinalIgnoreCase))
                {
                    request.Lote = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Division", StringComparison.OrdinalIgnoreCase))
                {
                    request.Division = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Phv", StringComparison.OrdinalIgnoreCase))
                {
                    request.Phv = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Phh", StringComparison.OrdinalIgnoreCase))
                {
                    request.Phh = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Numero", StringComparison.OrdinalIgnoreCase))
                {
                    request.Numero = detalleElement.Valor;
                }
            }
            return request;
        }

        private DimensionesRequest GenerarRequestDimensiones(ICollection<TmsFormularioDetalle> formularioDetalleList)
        {
            DimensionesRequest request = new();

            foreach (var detalleElement in formularioDetalleList)
            {
                if (String.Equals(detalleElement.Caracteristica, "Sector", StringComparison.OrdinalIgnoreCase))
                {
                    request.IdSector = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Manzana", StringComparison.OrdinalIgnoreCase))
                {
                    request.Manzana = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Lote", StringComparison.OrdinalIgnoreCase))
                {
                    request.Lote = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Division", StringComparison.OrdinalIgnoreCase))
                {
                    request.Division = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Phv", StringComparison.OrdinalIgnoreCase))
                {
                    request.Phv = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Phh", StringComparison.OrdinalIgnoreCase))
                {
                    request.Phh = detalleElement.Valor;
                }
                else if (String.Equals(detalleElement.Caracteristica, "Numero", StringComparison.OrdinalIgnoreCase))
                {
                    request.Numero = detalleElement.Valor;
                }
            }
            return request;
        }

    }
}
