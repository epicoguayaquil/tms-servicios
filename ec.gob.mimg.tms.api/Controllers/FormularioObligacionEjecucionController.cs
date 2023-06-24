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
    public class FormularioObligacionEjecucionController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IFormularioObligacionEjecucionService _formularioObligacionEjecucionService;

        private readonly IMapper _mapper;

        public FormularioObligacionEjecucionController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _formularioObligacionEjecucionService = new FormularioObligacionEjecucionService(_dbContext);
        }

        // GET: api/FormularioObligacionEjecucion
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var formularioObligacionEjecucionList = await _formularioObligacionEjecucionService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioObligacionEjecucionList.Select(x => _mapper.Map<FormularioObligacionEjecucionResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/FormularioObligacionEjecucion/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formularioObligacionEjecucion = await _formularioObligacionEjecucionService.GetById(id);

            if (formularioObligacionEjecucion == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<FormularioObligacionEjecucionResponse>(formularioObligacionEjecucion)
                };
                return Ok(response);
            }
        }

        // POST: api/FormularioObligacionEjecucion
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioObligacionEjecucionRequest formularioObligacionEjecucionRequest)
        {
            try
            {
                TmsFormularioObligacionEjecucion formularioObligacionEjecucion = new();
                formularioObligacionEjecucion = _mapper.Map<TmsFormularioObligacionEjecucion>(formularioObligacionEjecucionRequest);
                formularioObligacionEjecucion.FechaEjecucion = DateTime.Now;
                formularioObligacionEjecucion.FechaRegistro = DateTime.Now;
                formularioObligacionEjecucion.UsuarioRegistro = "admin@mail.com";
                formularioObligacionEjecucion.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioObligacionEjecucionService.AddAsync(formularioObligacionEjecucion);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionEjecucionResponse>(formularioObligacionEjecucion)
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

        // DELETE: api/FormularioObligacionEjecucion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formularioObligacionEjecucion = await _formularioObligacionEjecucionService.GetById(id);

            if (formularioObligacionEjecucion == null)
            {
                return NotFound();
            }

            await _formularioObligacionEjecucionService.DeleteAsync(formularioObligacionEjecucion);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

    }
}
