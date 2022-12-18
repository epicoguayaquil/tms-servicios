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
    public class FormularioController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly FormularioService _formularioService;
        private readonly FormularioDetalleService _formularioDetalleService;
        private readonly FormularioActividadService _formularioActividadService;

        private readonly IMapper _mapper;

        public FormularioController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _formularioService = new FormularioService(_dbContext);
            _formularioDetalleService = new FormularioDetalleService(_dbContext);
            _formularioActividadService = new FormularioActividadService(_dbContext);
        }

        // GET: api/Formulario
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var formularioList = await _formularioService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioList.Select(x => _mapper.Map<FormularioResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Formulario/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formulario =await _formularioService.GetFirstOrDefaultById(id);

            if (formulario == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<FormularioResponse>(formulario)
                };
                return Ok(response);
            }
        }

        // GET: api/Formulario/1/detalles
        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<GenericResponse>> GetAllDetallesById(int id)
        {
            var formularioDetalleList = await _formularioDetalleService.GetAsync(x => x.FormularioId == id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioDetalleList.Select(x => _mapper.Map<FormularioDetalleResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Formulario/1/detallesNivel/2
        [HttpGet("{id}/detallesNivel/{pasoCreacion}")]
        public async Task<ActionResult<GenericResponse>> GetAllDetallesNivelById(int id, int pasoCreacion)
        {
            var formularioDetalleList = await _formularioDetalleService.GetAsync(x => (x.FormularioId == id
                    && x.PasoCreacion == pasoCreacion));
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioDetalleList.Select(x => _mapper.Map<FormularioDetalleResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Formulario/1/actividades
        [HttpGet("{id}/actividades")]
        public async Task<ActionResult<GenericResponse>> GetAllActividadesById(int id)
        {
            var formularioActividadList = await _formularioActividadService.GetAsync(x => x.FormularioId == id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioActividadList.Select(x => _mapper.Map<FormularioActividadResponse>(x))
            };

            return Ok(response);
        }

        // POST: api/Formulario
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioRequest formularioRequest)
        {
            try
            {
                TmsFormulario formulario = new TmsFormulario();
                formulario = _mapper.Map<TmsFormulario>(formularioRequest);
                formulario.FechaRegistro = DateTime.Now;
                formulario.UsuarioRegistro = "admin@mail.com";
                formulario.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioService.AddAsync(formulario);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioResponse>(formulario)
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

        // DELETE: api/Formulario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formulario = await _formularioService.GetFirstOrDefaultAsync(x => x.IdFormulario == id);

            if (formulario == null)
            {
                return NotFound();
            }

            await _formularioService.DeleteAsync(formulario);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/Formulario
        [HttpPut]
        public async Task<IActionResult> Update(FormularioRequest formularioRequest)
        {
            try
            {
                var formularioActual = await _formularioService.GetFirstOrDefaultAsync(x => x.IdFormulario == formularioRequest.IdFormulario);

                if (formularioActual == null) { return NotFound(); }

                TmsFormulario formulario = new TmsFormulario();
                formulario = _mapper.Map<TmsFormulario>(formularioRequest);

                formulario.FechaRegistro = formularioActual.FechaRegistro;
                formulario.UsuarioRegistro = formularioActual.UsuarioRegistro;
                formulario.Estado = formularioActual.Estado;
                formulario.FechaModificacion = DateTime.Now;
                formulario.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _formularioService.UpdateAsync(formulario);
                    
                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioResponse>(formulario)
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
