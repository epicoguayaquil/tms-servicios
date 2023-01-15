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
    public class FormularioActividadController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IFormularioActividadService _formularioActividadService;

        private readonly IMapper _mapper;

        public FormularioActividadController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _formularioActividadService = new FormularioActividadService(_dbContext);
        }

        // GET: api/FormularioActividad
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var formularioActividadList = await _formularioActividadService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioActividadList.Select(x => _mapper.Map<FormularioActividadResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/FormularioActividad/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formularioActividad = await _formularioActividadService.GetById(id);

            if (formularioActividad == null)
            {
                return NotFound();
            }
            else
            {
                FormularioActividadResponse formularioActividadResponse = _mapper.Map<FormularioActividadResponse>(formularioActividad);
                formularioActividadResponse.ActividadEconomica = _mapper.Map<ActividadEconomicaResponse>(formularioActividad.ActividadEconomica);
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = formularioActividadResponse
                };
                return Ok(response);
            }
        }

        // POST: api/FormularioActividad
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioActividadRequest formularioActividadRequest)
        {
            try
            {
                TmsFormularioActividad formularioActividad = new TmsFormularioActividad();
                formularioActividad = _mapper.Map<TmsFormularioActividad>(formularioActividadRequest);
                formularioActividad.FechaRegistro = DateTime.Now;
                formularioActividad.UsuarioRegistro = "admin@mail.com";
                formularioActividad.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioActividadService.AddAsync(formularioActividad);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioActividadResponse>(formularioActividad)
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

        // DELETE: api/FormularioActividad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formularioActividad = await _formularioActividadService.GetById(id);

            if (formularioActividad == null)
            {
                return NotFound();
            }

            await _formularioActividadService.DeleteAsync(formularioActividad);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/FormularioActividad/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FormularioActividadRequest formularioActividadRequest)
        {
            try
            {
                var formularioActividadActual = await _formularioActividadService.GetById(id);
                if (formularioActividadActual == null) { return NotFound(); }

                TmsFormularioActividad formularioActividad = new TmsFormularioActividad();
                formularioActividad = _mapper.Map<TmsFormularioActividad>(formularioActividadRequest);

                formularioActividad.FechaRegistro = formularioActividadActual.FechaRegistro;
                formularioActividad.UsuarioRegistro = formularioActividadActual.UsuarioRegistro;
                formularioActividad.Estado = formularioActividadActual.Estado;
                formularioActividad.FechaModificacion = DateTime.Now;
                formularioActividad.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _formularioActividadService.UpdateAsync(formularioActividad);
                    
                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioActividadResponse>(formularioActividad)
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
