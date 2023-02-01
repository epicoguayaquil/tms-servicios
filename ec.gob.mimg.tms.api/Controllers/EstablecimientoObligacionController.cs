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
    public class EstablecimientoObligacionController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IEstablecimientoObligacionService _establecimientoObligacionService;

        private readonly IMapper _mapper;

        public EstablecimientoObligacionController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _establecimientoObligacionService = new EstablecimientoObligacionService(_dbContext);
        }

        // GET: api/EstablecimientoObligacion
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var establecimientoObligacionList = await _establecimientoObligacionService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = establecimientoObligacionList.Select(x => _mapper.Map<EstablecimientoObligacionResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/EstablecimientoObligacion/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var establecimientoObligacion = await _establecimientoObligacionService.GetById(id);

            if (establecimientoObligacion == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EstablecimientoObligacionResponse>(establecimientoObligacion)
                };
                return Ok(response);
            }
        }

        // POST: api/EstablecimientoObligacion
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(EstablecimientoObligacionRequest establecimientoObligacionRequest)
        {
            try
            {
                TmsEstablecimientoObligacion establecimientoObligacion = new TmsEstablecimientoObligacion();
                establecimientoObligacion = _mapper.Map<TmsEstablecimientoObligacion>(establecimientoObligacionRequest);
                establecimientoObligacion.FechaRegistro = DateTime.Now;
                establecimientoObligacion.UsuarioRegistro = "admin@mail.com";
                //establecimientoObligacion.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _establecimientoObligacionService.AddAsync(establecimientoObligacion);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EstablecimientoObligacionResponse>(establecimientoObligacion)
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

        // DELETE: api/EstablecimientoObligacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var establecimientoObligacion = await _establecimientoObligacionService.GetById(id);

            if (establecimientoObligacion == null)
            {
                return NotFound();
            }

            await _establecimientoObligacionService.DeleteAsync(establecimientoObligacion);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/EstablecimientoObligacion/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EstablecimientoObligacionRequest establecimientoObligacionRequest)
        {
            try
            {
                var establecimientoObligacionActual = await _establecimientoObligacionService.GetById(id);
                if (establecimientoObligacionActual == null) { return NotFound(); }

                establecimientoObligacionActual.ObligacionId = establecimientoObligacionRequest.ObligacionId;
                establecimientoObligacionActual.EstablecimientoId = establecimientoObligacionRequest.EstablecimientoId;
                establecimientoObligacionActual.FechaModificacion = DateTime.Now;
                establecimientoObligacionActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _establecimientoObligacionService.UpdateAsync(establecimientoObligacionActual);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EstablecimientoObligacionResponse>(establecimientoObligacionActual)
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
