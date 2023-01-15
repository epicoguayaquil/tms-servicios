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
    public class ObligacionActividadController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IObligacionActividadService _obligacionActividadService;

        private readonly IMapper _mapper;

        public ObligacionActividadController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _obligacionActividadService = new ObligacionActividadService(_dbContext);
        }

        // GET: api/ObligacionActividad
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var obligacionActividadList = await _obligacionActividadService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionActividadList.Select(x => _mapper.Map<ObligacionActividadResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/ObligacionActividad/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var obligacionActividad = await _obligacionActividadService.GetById(id);

            if (obligacionActividad == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<ObligacionActividadResponse>(obligacionActividad)
                };
                return Ok(response);
            }
        }

        // POST: api/ObligacionActividad
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(ObligacionActividadRequest obligacionActividadRequest)
        {
            try
            {
                TmsActividadObligacion obligacionActividad = new TmsActividadObligacion();
                obligacionActividad = _mapper.Map<TmsActividadObligacion>(obligacionActividadRequest);
                obligacionActividad.FechaRegistro = DateTime.Now;
                obligacionActividad.UsuarioRegistro = "admin@mail.com";
                obligacionActividad.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _obligacionActividadService.AddAsync(obligacionActividad);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<ObligacionActividadResponse>(obligacionActividad)
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

        // DELETE: api/ObligacionActividad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obligacionActividad = await _obligacionActividadService.GetById(id);

            if (obligacionActividad == null)
            {
                return NotFound();
            }

            await _obligacionActividadService.DeleteAsync(obligacionActividad);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/ObligacionActividad/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ObligacionActividadRequest obligacionActividadRequest)
        {
            try
            {
                var obligacionActividadActual = await _obligacionActividadService.GetById(id);
                if (obligacionActividadActual == null) { return NotFound(); }

                obligacionActividadActual.FechaModificacion = DateTime.Now;
                obligacionActividadActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _obligacionActividadService.UpdateAsync(obligacionActividadActual);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<ObligacionActividadResponse>(obligacionActividadActual)
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
