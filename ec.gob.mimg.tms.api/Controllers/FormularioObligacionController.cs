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
    public class FormularioObligacionController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IFormularioObligacionService _formularioObligacionService;
        private readonly IFormularioObligacionCaracteristicaValorService _formularioObligacionCaracteristicaValorService;

        private readonly IMapper _mapper;

        public FormularioObligacionController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _formularioObligacionService = new FormularioObligacionService(_dbContext);
            _formularioObligacionCaracteristicaValorService = new FormularioObligacionCaracteristicaValorService(_dbContext);
        }

        // GET: api/FormularioObligacion
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var formularioObligacionList = await _formularioObligacionService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioObligacionList.Select(x => _mapper.Map<FormularioObligacionResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/FormularioObligacion/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formularioObligacion = await _formularioObligacionService.GetById(id);

            if (formularioObligacion == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<FormularioObligacionResponse>(formularioObligacion)
                };
                return Ok(response);
            }
        }

        // POST: api/FormularioObligacion
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioObligacionRequest formularioObligacionRequest)
        {
            try
            {
                TmsFormularioObligacion formularioObligacion = new TmsFormularioObligacion();
                formularioObligacion = _mapper.Map<TmsFormularioObligacion>(formularioObligacionRequest);
                formularioObligacion.FechaRegistro = DateTime.Now;
                formularioObligacion.UsuarioRegistro = "admin@mail.com";
                //formularioObligacion.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioObligacionService.AddAsync(formularioObligacion);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionResponse>(formularioObligacion)
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

        // DELETE: api/FormularioObligacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formularioObligacion = await _formularioObligacionService.GetById(id);

            if (formularioObligacion == null)
            {
                return NotFound();
            }

            await _formularioObligacionService.DeleteAsync(formularioObligacion);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/FormularioObligacion/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FormularioObligacionRequest formularioObligacionRequest)
        {
            try
            {
                var formularioObligacionActual = await _formularioObligacionService.GetById(id);
                if (formularioObligacionActual == null) { return NotFound(); }

                formularioObligacionActual.ObligacionId = formularioObligacionRequest.ObligacionId;
                formularioObligacionActual.FormularioId = formularioObligacionRequest.FormularioId;
                formularioObligacionActual.FechaModificacion = DateTime.Now;
                formularioObligacionActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _formularioObligacionService.UpdateAsync(formularioObligacionActual);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionResponse>(formularioObligacionActual)
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

        // GET: api/FormularioObligacion/1/caracteristicas
        [HttpGet("{id}/caracteristicas")]
        public async Task<ActionResult<GenericResponse>> GetAllCaracteristicasById(int id)
        {
            var formularioObligacionCaracteristicaValorList = await _formularioObligacionCaracteristicaValorService.GetListByFormularioObligacionId(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioObligacionCaracteristicaValorList.Select(x => _mapper.Map<FormularioObligacionCaracteristicaValorResponse>(x))
            };

            return Ok(response);
        }

    }
}
