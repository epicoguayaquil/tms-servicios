﻿using Microsoft.AspNetCore.Mvc;
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
    public class EstablecimientoController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IEstablecimientoService _establecimientoService;
        private readonly IFormularioService _formularioService;
        private readonly IFormularioActividadService _formularioActividadService;
        private readonly IActividadEconomicaService _actividadEconomicaService;
        private readonly IFormularioDetalleService _formularioDetalleService;
        private readonly IRegistroNotificacionService _registroNotificacionService;

        private readonly IMapper _mapper;

        public EstablecimientoController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _establecimientoService = new EstablecimientoService(_dbContext);
            _formularioService = new FormularioService(_dbContext);
            _formularioActividadService = new FormularioActividadService(_dbContext);
            _actividadEconomicaService = new ActividadEconomicaService(_dbContext);
            _formularioDetalleService = new FormularioDetalleService(_dbContext);
            _registroNotificacionService = new RegistroNotificacionService(_dbContext);
        }

        // GET: api/Establecimiento
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {

            var establecimientoList = await _establecimientoService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = establecimientoList.Select(x => _mapper.Map<EstablecimientoResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Establecimiento/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var establecimiento = await _establecimientoService.GetById(id);

            if (establecimiento == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EstablecimientoResponse>(establecimiento)
                };
                return Ok(response);
            }
        }

        // POST: api/Establecimiento
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(EstablecimientoRequest establecimientoRequest)
        {
            try
            {
                TmsEstablecimiento establecimiento = new TmsEstablecimiento();
                establecimiento = _mapper.Map<TmsEstablecimiento>(establecimientoRequest);
                establecimiento.FechaRegistro = DateTime.Now;
                establecimiento.UsuarioRegistro = "admin@mail.com";
                establecimiento.Estado = EstadoEstablecimientoEnum.INHABILITADO.ToString();
                establecimiento.EstadoRegistro = EstadoRegistroEnum.NO_REGISTRADO.ToString();

                bool isSaved = await _establecimientoService.AddAsync(establecimiento);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EstablecimientoResponse>(establecimiento)
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

        // DELETE: api/Establecimiento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var establecimiento = await _establecimientoService.GetById(id);

            if (establecimiento == null)
            {
                return NotFound();
            }

            await _establecimientoService.DeleteAsync(establecimiento);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/Establecimiento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EstablecimientoRequest establecimientoRequest)
        {
            try
            {
                var establecimientoActual = await _establecimientoService.GetById(id);

                if (establecimientoActual == null) { return NotFound(); }

                TmsEstablecimiento establecimiento = new TmsEstablecimiento();
                establecimiento = _mapper.Map<TmsEstablecimiento>(establecimientoRequest);

                establecimiento.IdEstablecimiento = id;
                establecimiento.FechaRegistro = establecimientoActual.FechaRegistro;
                establecimiento.UsuarioRegistro = establecimientoActual.UsuarioRegistro;
                establecimiento.Estado = establecimientoActual.Estado;
                establecimiento.EstadoRegistro = establecimientoActual.EstadoRegistro;
                establecimiento.FechaModificacion = DateTime.Now;
                establecimiento.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _establecimientoService.UpdateAsync(establecimiento);
                    
                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EstablecimientoResponse>(establecimiento)
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

        // GET: api/Establecimiento/1/formularioActivo
        [HttpGet("{id}/formularioActivo")]
        public async Task<ActionResult<GenericResponse>> GetFormularioActivoById(int id)
        {
            var formularioActivoList = await _formularioService.GetListByEstablecimientoIdAndEstado(id, EstadoEnum.ACTIVO.ToString());
            TmsFormulario formulario;
            if (formularioActivoList.Count == 0)
            {
                formulario = new TmsFormulario
                {
                    EstablecimientoId = id,
                    PasoCreacionActual = 0,
                    FechaRegistro = DateTime.Now,
                    UsuarioRegistro = "admin@mail.com",
                    Estado = EstadoEnum.ACTIVO.ToString()
                };
                bool isSaved = await _formularioService.AddAsync(formulario);
                if (!isSaved)
                {
                    return NotFound();
                }
            }
            else
            {
                formulario = formularioActivoList.First();
            }

            FormularioResponse formularioResponse = _mapper.Map<FormularioResponse>(formulario);
            var formularioActividadList = await _formularioActividadService.GetListByFormularioId(formulario.IdFormulario);
            var formularioActividadResponseList = formularioActividadList.Select(x => _mapper.Map<FormularioActividadResponse>(x));
            var formularioActividadResponseListNew = new List<FormularioActividadResponse>();
            foreach (var formularioActividadResponse in formularioActividadResponseList)
            {
                var actividadEconomica = await _actividadEconomicaService.GetById(formularioActividadResponse.ActividadEconomicaId);
                formularioActividadResponse.ActividadEconomica = _mapper.Map<ActividadEconomicaResponse>(actividadEconomica);
                formularioActividadResponseListNew.Add(formularioActividadResponse);
            }
            formularioResponse.FormularioActividadResponseList = formularioActividadResponseListNew;

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioResponse
            };
            return Ok(response);
        }

        // GET: api/Establecimiento/1/formularioRenovacion
        [HttpGet("{id}/formularioRenovacion")]
        public async Task<ActionResult<GenericResponse>> GetFormularioParaRenovarById(int id)
        {
            TmsFormulario formulario;
            var formularioEnProcesoList = await _formularioService.GetListByEstablecimientoIdAndEstado(id, EstadoEnum.EN_PROCESO.ToString());
            if (formularioEnProcesoList.Count > 0)
            {
                formulario = formularioEnProcesoList.First();
            }
            else
            {
                var formularioActivoList = await _formularioService.GetListByEstablecimientoIdAndEstado(id, EstadoEnum.ACTIVO.ToString());
                if (formularioActivoList.Count == 0)
                {
                    formulario = new TmsFormulario
                    {
                        EstablecimientoId = id,
                        PasoCreacionActual = 0,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "admin@mail.com",
                        Estado = EstadoEnum.ACTIVO.ToString()
                    };
                    bool isSaved = await _formularioService.AddAsync(formulario);
                    if (!isSaved)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    formulario = new TmsFormulario
                    {
                        EstablecimientoId = id,
                        PasoCreacionActual = 0,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "admin@mail.com",
                        Estado = EstadoEnum.EN_PROCESO.ToString()
                    };
                    bool isSaved = await _formularioService.AddAsync(formulario);
                    if (!isSaved)
                    {
                        return NotFound();
                    }

                    TmsFormulario formularioActivo = formularioActivoList.First();

                    var formularioActividadListActivo = await _formularioActividadService.GetListByFormularioId(formularioActivo.IdFormulario);
                    foreach(TmsFormularioActividad formularioActividadIni in formularioActividadListActivo)
                    {
                        TmsFormularioActividad formularioActividad = new()
                        {
                            FormularioId = formulario.IdFormulario,
                            ActividadEconomicaId = formularioActividadIni.ActividadEconomicaId,
                            Observacion = formularioActividadIni.Observacion,
                            Estado = EstadoEnum.ACTIVO.ToString(),
                            UsuarioRegistro = "admin@mail.com",
                            FechaRegistro = DateTime.Now
                        };
                        await _formularioActividadService.AddAsync(formularioActividad);
                    }

                    var formularioDetalleList = await _formularioDetalleService.GetListByFormularioId(formularioActivo.IdFormulario);
                    foreach (TmsFormularioDetalle formularioDetalleIni in formularioDetalleList)
                    {
                        TmsFormularioDetalle formularioDetalle = new()
                        {
                            FormularioId = formulario.IdFormulario,
                            Caracteristica = formularioDetalleIni.Caracteristica,
                            Valor = formularioDetalleIni.Valor,
                            TipoDato = formularioDetalleIni.TipoDato,
                            ExtraInfo = formularioDetalleIni.ExtraInfo,
                            PasoCreacion = formularioDetalleIni.PasoCreacion,
                            FechaRegistro = DateTime.Now,
                            UsuarioRegistro = "admin@mail.com"
                        };
                        await _formularioDetalleService.AddAsync(formularioDetalle);

                    }

                }
            }
            FormularioResponse formularioResponse = _mapper.Map<FormularioResponse>(formulario);
            var formularioActividadList = await _formularioActividadService.GetListByFormularioId(formulario.IdFormulario);
            var formularioActividadResponseList = formularioActividadList.Select(x => _mapper.Map<FormularioActividadResponse>(x));
            var formularioActividadResponseListNew = new List<FormularioActividadResponse>();
            foreach (var formularioActividadResponse in formularioActividadResponseList)
            {
                var actividadEconomica = await _actividadEconomicaService.GetById(formularioActividadResponse.ActividadEconomicaId);
                formularioActividadResponse.ActividadEconomica = _mapper.Map<ActividadEconomicaResponse>(actividadEconomica);
                formularioActividadResponseListNew.Add(formularioActividadResponse);
            }
            formularioResponse.FormularioActividadResponseList = formularioActividadResponseListNew;

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioResponse
            };
            return Ok(response);
        }

        // PUT: api/Establecimiento/nombreComercial
        [HttpPut("nombreComercial")]
        public async Task<IActionResult> UpdateNombreComercial(EstablecimientoExtraDataRequest establecimientoRequest)
        {
            try
            {
                var establecimientoActual = await _establecimientoService.GetById(establecimientoRequest.IdEstablecimiento);

                if (establecimientoActual == null) { return NotFound(); }

                establecimientoActual.NombreComercial = establecimientoRequest.NombreComercial;
                establecimientoActual.Industria = establecimientoRequest.Industria;
                establecimientoActual.FechaModificacion = DateTime.Now;
                establecimientoActual.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _establecimientoService.UpdateAsync(establecimientoActual);

                if (!isSaved)
                {
                    return BadRequest();
                }

                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EstablecimientoResponse>(establecimientoActual)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }
        
        // GET: api/Establecimiento/1/formularios
        [HttpGet("{id}/formularios")]
        public async Task<ActionResult<GenericResponse>> GetAllFormulariosById(int id)
        {
            var formularioList = await _formularioService.GetListByEstablecimientoId(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioList.Select(x => _mapper.Map<FormularioResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Establecimiento/1/formulariosActivos
        [HttpGet("{id}/formulariosActivos")]
        public async Task<ActionResult<GenericResponse>> GetAllFormulariosActivosById(int id)
        {
            var formularioList = await _formularioService.GetListByEstablecimientoIdAndEstado(id,
                                                    EstadoEnum.ACTIVO.ToString());
            List<FormularioResponse> formularioResponseList = new List<FormularioResponse>();
            foreach (var formulario in formularioList)
            {
                FormularioResponse formularioResponse = _mapper.Map<FormularioResponse>(formulario);
                foreach (var formularioActividad in formulario.TmsFormularioActividads)
                {
                    FormularioActividadResponse formularioActividadResponse = _mapper.Map<FormularioActividadResponse>(formularioActividad);
                    formularioActividadResponse.ActividadEconomica = _mapper.Map<ActividadEconomicaResponse>(formularioActividad.ActividadEconomica);
                    formularioResponse.FormularioActividadResponseList.Append(formularioActividadResponse);
                }
                formularioResponseList.Add(formularioResponse);
            }

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioResponseList
            };

            return Ok(response);
        }

        // GET: api/Establecimiento/5/notificaciones
        [HttpGet("{id}/notificaciones")]
        public async Task<ActionResult<GenericResponse>> GetNotificacionesById(int id)
        {
            var notificacionList = await _registroNotificacionService.GetListByJerarquiaAndObjetoId("Establecimiento", id);
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
