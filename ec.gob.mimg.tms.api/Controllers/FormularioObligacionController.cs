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

                var formularioActividadList = await _formularioActividadService.GetListByFormularioId(id);
                TmsFormularioActividad formularioActividad = formularioActividadList.First();
                request.IdActividad = formularioActividad.ActividadEconomicaId.ToString();
                var responseUsoSuelo = await _apiMimgService.GetFacilidadUso(request);

                if (!responseUsoSuelo.DataResult)
                {
                    response.Cod = "500";
                    response.Msg = "FAIL";
                    response.Data = responseUsoSuelo.Resultado;
                } else
                {
                    formularioObligacion.Estado = EstadoObligacionEnum.CUMPLE.ToString();
                    formularioObligacion.FechaModificacion = DateTime.Now;
                    formularioObligacion.UsuarioModificacion = "admin@mail.com";

                    bool isUpdate = await _formularioObligacionService.UpdateAsync(formularioObligacion);

                    if (isUpdate)
                    {
                        response.Data = "Cumplió";
                    }
                    else
                    {
                        return BadRequest();
                    }
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

                var formularioActividadList = await _formularioActividadService.GetListByFormularioId(id);
                TmsFormularioActividad formularioActividad = formularioActividadList.First();
                request.IdActividad = formularioActividad.ActividadEconomicaId.ToString();
                var responseDimensiones = await _apiMimgService.GetDimensionMinima(request);

                response.Data = responseDimensiones;
                /*if (!responseDimensiones.DataResult)
                {
                    response.Cod = "500";
                    response.Msg = "FAIL";
                    response.Data = responseDimensiones.Resultado;
                }
                else
                {
                    formularioObligacion.Estado = EstadoObligacionEnum.CUMPLE.ToString();
                    formularioObligacion.FechaModificacion = DateTime.Now;
                    formularioObligacion.UsuarioModificacion = "admin@mail.com";

                    bool isUpdate = await _formularioObligacionService.UpdateAsync(formularioObligacion);

                    if (isUpdate)
                    {
                        response.Data = "Cumplió";
                    }
                    else
                    {
                        return BadRequest();
                    }
                }*/
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
