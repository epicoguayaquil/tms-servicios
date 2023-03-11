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
    public class RegistroNotificacionController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IRegistroNotificacionService _registroNotificacionService;

        private readonly IMapper _mapper;

        public RegistroNotificacionController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _registroNotificacionService = new RegistroNotificacionService(_dbContext);
        }

        // GET: api/Notificacion
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var notificacionList = await _registroNotificacionService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = notificacionList.Select(x => _mapper.Map<RegistroNotificacionResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Notificacion/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var notificacion = await _registroNotificacionService.GetById(id);

            if (notificacion == null)
            {
                return NotFound();
            }
            else
            {
                RegistroNotificacionResponse notificacionResponse = _mapper.Map<RegistroNotificacionResponse>(notificacion);
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = notificacionResponse
                };
                return Ok(response);
            }
        }

        // POST: api/Notificacion
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(RegistroNotificacionRequest notificacionRequest)
        {
            try
            {
                TmsNotificacion notificacion = new TmsNotificacion();
                notificacion = _mapper.Map<TmsNotificacion>(notificacionRequest);
                notificacion.FechaRegistro = DateTime.Now;
                notificacion.UsuarioRegistro = "admin@mail.com";
                notificacion.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _registroNotificacionService.AddAsync(notificacion);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<RegistroNotificacionResponse>(notificacion)
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

        // DELETE: api/Notificacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notificacion = await _registroNotificacionService.GetById(id);

            if (notificacion == null)
            {
                return NotFound();
            }

            await _registroNotificacionService.DeleteAsync(notificacion);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/Notificacion/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RegistroNotificacionRequest notificacionRequest)
        {
            try
            {
                var notificacionActual = await _registroNotificacionService.GetById(id);
                if (notificacionActual == null) { return NotFound(); }

                TmsNotificacion notificacion = new TmsNotificacion();
                notificacion = _mapper.Map<TmsNotificacion>(notificacionRequest);

                notificacion.IdNotificacion = id;
                notificacion.FechaRegistro = notificacionActual.FechaRegistro;
                notificacion.UsuarioRegistro = notificacionActual.UsuarioRegistro;
                notificacion.Estado = notificacionActual.Estado;
                notificacion.FechaModificacion = DateTime.Now;
                notificacion.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _registroNotificacionService.UpdateAsync(notificacion);
                    
                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<RegistroNotificacionResponse>(notificacion)
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
