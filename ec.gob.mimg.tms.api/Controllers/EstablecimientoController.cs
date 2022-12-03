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
    public class EstablecimientoController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly EstablecimientoService _establecimientoService;

        private readonly IMapper _mapper;

        public EstablecimientoController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _establecimientoService = new EstablecimientoService(_dbContext);
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
            var establecimiento = await _establecimientoService.GetFirstOrDefaultAsync(x => x.IdEstablecimiento == id);

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
                    Data = _mapper.Map<EmpresaResponse>(establecimiento)
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
                establecimiento.Estado = EstadoEnum.ACTIVO.ToString();

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
            var establecimiento = await _establecimientoService.GetFirstOrDefaultAsync(x => x.IdEstablecimiento == id);

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

        // PUT: api/Establecimiento
        [HttpPut]
        public async Task<IActionResult> Update(EstablecimientoRequest establecimientoRequest)
        {
            try
            {
                var establecimientoActual = await _establecimientoService.GetFirstOrDefaultAsync(x => x.IdEstablecimiento == establecimientoRequest.IdEstablecimiento);

                if (establecimientoActual == null) { return NotFound(); }

                TmsEstablecimiento establecimiento = new TmsEstablecimiento();
                establecimiento = _mapper.Map<TmsEstablecimiento>(establecimientoRequest);

                establecimiento.FechaRegistro = establecimientoActual.FechaRegistro;
                establecimiento.UsuarioRegistro = establecimientoActual.UsuarioRegistro;
                establecimiento.Estado = establecimientoActual.Estado;
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
    }
}
