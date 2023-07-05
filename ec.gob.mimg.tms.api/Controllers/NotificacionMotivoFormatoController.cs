using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class NotificacionMotivoFormatoController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly INotificacionMotivoFormatoService _notificacionMotivoFormatoService;

        private readonly IMapper _mapper;

        public NotificacionMotivoFormatoController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _notificacionMotivoFormatoService = new NotificacionMotivoFormatoService(_dbContext);
        }

        // GET: api/NotificacionMotivoFormato
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var notificacionList = await _notificacionMotivoFormatoService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = notificacionList.Select(x => _mapper.Map<NotificacionMotivoFormatoResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/NotificacionMotivoFormato/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var notificacionMotivo = await _notificacionMotivoFormatoService.GetById(id);

            if (notificacionMotivo == null)
            {
                return NotFound();
            }
            else
            {
                NotificacionMotivoFormatoResponse notificacionResponse = _mapper.Map<NotificacionMotivoFormatoResponse>(notificacionMotivo);
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = notificacionResponse
                };
                return Ok(response);
            }
        }

        // GET: api/NotificacionMotivoFormato/byMotivo/NUEVO_ESTABLECIMIENTO
        [HttpGet("byMotivo/{motivo}")]
        public async Task<ActionResult<GenericResponse>> GetByMotivo(string motivo)
        {
            var notificacionMotivo = await _notificacionMotivoFormatoService.GetByMotivo(motivo);

            if (notificacionMotivo == null)
            {
                return NotFound();
            }
            else
            {
                NotificacionMotivoFormatoResponse notificacionResponse = _mapper.Map<NotificacionMotivoFormatoResponse>(notificacionMotivo);
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = notificacionResponse
                };
                return Ok(response);
            }
        }
        // POST: api/NotificacionMotivoFormato
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(NotificacionMotivoFormatoRequest notificacionMotivoRequest)
        {
            try
            {
                TmsNotificacionMotivoFormato notificacionMotivo = new TmsNotificacionMotivoFormato();
                notificacionMotivo = _mapper.Map<TmsNotificacionMotivoFormato>(notificacionMotivoRequest);
                notificacionMotivo.FechaRegistro = DateTime.Now;
                notificacionMotivo.UsuarioRegistro = "admin@mail.com";
                notificacionMotivo.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _notificacionMotivoFormatoService.AddAsync(notificacionMotivo);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<NotificacionMotivoFormatoResponse>(notificacionMotivo)
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

        // DELETE: api/NotificacionMotivoFormato/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notificacionMotivo = await _notificacionMotivoFormatoService.GetById(id);

            if (notificacionMotivo == null)
            {
                return NotFound();
            }

            await _notificacionMotivoFormatoService.DeleteAsync(notificacionMotivo);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/NotificacionMotivoFormato/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NotificacionMotivoFormatoRequest notificacionMotivoRequest)
        {
            try
            {
                var notificacionMotivoActual = await _notificacionMotivoFormatoService.GetById(id);
                if (notificacionMotivoActual == null) { return NotFound(); }

                TmsNotificacionMotivoFormato notificacionMotivo = new TmsNotificacionMotivoFormato();
                notificacionMotivo = _mapper.Map<TmsNotificacionMotivoFormato>(notificacionMotivoRequest);

                notificacionMotivo.IdNotificacionMotivoFormato = id;
                notificacionMotivo.FechaRegistro = notificacionMotivoActual.FechaRegistro;
                notificacionMotivo.UsuarioRegistro = notificacionMotivoActual.UsuarioRegistro;
                notificacionMotivo.Estado = notificacionMotivoActual.Estado;
                notificacionMotivo.FechaModificaion = DateTime.Now;
                notificacionMotivo.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _notificacionMotivoFormatoService.UpdateAsync(notificacionMotivo);
                    
                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<NotificacionMotivoFormatoResponse>(notificacionMotivo)
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
    }
}
