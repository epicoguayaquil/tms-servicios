using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Services.Implements;
using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.Services;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObligacionController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IObligacionService _obligacionService;
        private readonly IObligacionActividadService _obligacionActividadService;
        private readonly IObligacionCaracteristicaService _obligacionCaracteristicaService;
        private readonly IRegistroNotificacionService _registroNotificacionService;
        private readonly IObligacionDependenciaService _obligacionDependenciaService;

        private readonly IMapper _mapper;

        public ObligacionController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _obligacionService = new ObligacionService(_dbContext);
            _obligacionActividadService = new ObligacionActividadService(_dbContext);
            _obligacionCaracteristicaService = new ObligacionCaracteristicaService(_dbContext);
            _registroNotificacionService = new RegistroNotificacionService(_dbContext);
            _obligacionDependenciaService = new ObligacionDependenciaService(_dbContext);
        }

        // GET: api/Obligacion
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var obligacionList = await _obligacionService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionList.Select(x => _mapper.Map<ObligacionResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Obligacion/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var obligacion = await _obligacionService.GetById(id);

            if (obligacion == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<ObligacionResponse>(obligacion)
                };
                return Ok(response);
            }
        }

        // POST: api/Obligacion
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(ObligacionRequest obligacionRequest)
        {
            try
            {
                TmsObligacion obligacion = new TmsObligacion();
                obligacion = _mapper.Map<TmsObligacion>(obligacionRequest);
                obligacion.FechaRegistro = DateTime.Now;
                obligacion.UsuarioRegistro = "admin@mail.com";
                obligacion.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _obligacionService.AddAsync(obligacion);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<ObligacionResponse>(obligacion)
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

        // DELETE: api/Obligacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obligacion = await _obligacionService.GetById(id);

            if (obligacion == null)
            {
                return NotFound();
            }

            obligacion.Estado = EstadoEnum.INACTIVO.ToString();
            obligacion.FechaModificacion = DateTime.Now;
            obligacion.UsuarioModificacion = "admin@mail.com";

            bool isUpdate = await _obligacionService.UpdateAsync(obligacion);

            if (isUpdate)
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = "Eliminado"
                };
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Obligacion/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ObligacionRequest obligacionRequest)
        {
            try
            {
                var obligacionActual = await _obligacionService.GetById(id);
                if (obligacionActual == null) { return NotFound(); }

                obligacionActual.Nombre = obligacionRequest.Nombre;
                obligacionActual.TiempoVigencia = obligacionRequest.TiempoVigencia;
                obligacionActual.FechaExigibilidad = obligacionRequest.FechaExigibilidad;
                obligacionActual.FechaRenovacion = obligacionRequest.FechaRenovacion;
                obligacionActual.MesExigibilidad = obligacionRequest.MesExigibilidad;
                obligacionActual.MesRenovacion = obligacionRequest.MesRenovacion;
                obligacionActual.Origen = obligacionRequest.Origen;
                obligacionActual.Jerarquia = obligacionRequest.Jerarquia;
                obligacionActual.OrdenEjecucion = obligacionRequest.OrdenEjecucion;
                obligacionActual.Dependencia = obligacionRequest.Dependencia;
                obligacionActual.FechaModificacion = DateTime.Now;
                obligacionActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _obligacionService.UpdateAsync(obligacionActual);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<ObligacionResponse>(obligacionActual)
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

        // GET: api/Obligacion/1/actividades
        [HttpGet("{id}/actividades")]
        public async Task<ActionResult<GenericResponse>> GetAllActividadesById(int id)
        {
            var obligacionActividadList = await _obligacionActividadService.GetListByObligacionId(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionActividadList.Select(x => _mapper.Map<ObligacionActividadResponse>(x))
            };

            return Ok(response);
        }

        // POST: api/Obligacion/actividades
        [HttpPost("actividades")]
        public async Task<ActionResult<GenericResponse>> CreateListActividades(List<ObligacionActividadRequest> obligacionActividadListRequest)
        {
            try
            {
                foreach(ObligacionActividadRequest obligacionActividadRequest in obligacionActividadListRequest) {
                    TmsActividadObligacion obligacionActividad = new TmsActividadObligacion();
                    obligacionActividad = _mapper.Map<TmsActividadObligacion>(obligacionActividadRequest);
                    obligacionActividad.FechaRegistro = DateTime.Now;
                    obligacionActividad.UsuarioRegistro = "admin@mail.com";
                    obligacionActividad.Estado = EstadoEnum.ACTIVO.ToString();

                    bool isSaved = await _obligacionActividadService.AddAsync(obligacionActividad);

                    if (!isSaved)
                    {
                        return BadRequest();
                    }
                }
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = "Todos agregados"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        // GET: api/Obligacion/1/caracteristicas
        [HttpGet("{id}/caracteristicas")]
        public async Task<ActionResult<GenericResponse>> GetAllCaracteristicasById(int id)
        {
            var obligacionCaracteristicaList = await _obligacionCaracteristicaService.GetListByObligacionId(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionCaracteristicaList.Select(x => _mapper.Map<ObligacionCaracteristicaResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Obligacion/1/caracteristicas/FORMULARIO
        [HttpGet("{id}/caracteristicas/{tipo}")]
        public async Task<ActionResult<GenericResponse>> GetAllCaracteristicasByIdAndTipo(int id, string tipo)
        {
            var obligacionCaracteristicaList = await _obligacionCaracteristicaService.GetListByObligacionIdAndTipo(id, tipo);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionCaracteristicaList.Select(x => _mapper.Map<ObligacionCaracteristicaResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Obligacion/1/caracteristicas/FORMULARIO/Industria
        [HttpGet("{id}/caracteristicas/{tipo}/{subtipo}")]
        public async Task<ActionResult<GenericResponse>> GetAllCaracteristicasByIdAndTipoAndSubTipo(int id, string tipo, string subtipo)
        {
            var obligacionCaracteristicaList = await _obligacionCaracteristicaService.GetListByObligacionIdAndTipoANdSubTipo(id, tipo, subtipo);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionCaracteristicaList.Select(x => _mapper.Map<ObligacionCaracteristicaResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Obligacion/5/notificaciones
        [HttpGet("{id}/notificaciones")]
        public async Task<ActionResult<GenericResponse>> GetNotificacionesById(int id)
        {
            var notificacionList = await _registroNotificacionService.GetListByJerarquiaAndObjetoId("Obligacion", id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = notificacionList.Select(x => _mapper.Map<RegistroNotificacionResponse>(x))
            };
            return Ok(response);
        }

        // GET: api/Obligacion/5/dependecias/padres
        [HttpGet("{id}/dependecias/padres")]
        public async Task<ActionResult<GenericResponse>> GetDependenciasPadres(int id)
        {
            var padresList = await _obligacionDependenciaService.GetListaPadresByIdHijo(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = padresList.Select(x => _mapper.Map<ObligacionDependenciaResponse>(x))
            };
            return Ok(response);
        }

        // GET: api/Obligacion/5/dependecias/hijos
        [HttpGet("{id}/dependecias/hijos")]
        public async Task<ActionResult<GenericResponse>> GetDependenciasHijos(int id)
        {
            var padresList = await _obligacionDependenciaService.GetListaHijosByIdPadre(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = padresList.Select(x => _mapper.Map<ObligacionDependenciaResponse>(x))
            };
            return Ok(response);
        }
    }
}
