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
    public class FormularioObligacionCaracteristicaValorController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IFormularioObligacionCaracteristicaValorService _formularioObligacionCaracteristicaValorService;

        private readonly IMapper _mapper;

        public FormularioObligacionCaracteristicaValorController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _formularioObligacionCaracteristicaValorService = new FormularioObligacionCaracteristicaValorService(_dbContext);
        }

        // GET: api/FormularioObligacionCaracteristicaValor
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var obligacionActividadList = await _formularioObligacionCaracteristicaValorService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionActividadList.Select(x => _mapper.Map<FormularioObligacionCaracteristicaValorResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/FormularioObligacionCaracteristicaValor/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formularioObligacionCaracteristicaValor = await _formularioObligacionCaracteristicaValorService.GetById(id);

            if (formularioObligacionCaracteristicaValor == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<FormularioObligacionCaracteristicaValorResponse>(formularioObligacionCaracteristicaValor)
                };
                return Ok(response);
            }
        }

        // POST: api/FormularioObligacionCaracteristicaValor
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioObligacionCaracteristicaValorRequest formularioObligacionCaracteristicaValorRequest)
        {
            try
            {
                TmsFormularioObligacionCaracteristicaValor formularioObligacionCaracteristicaValor = new();
                formularioObligacionCaracteristicaValor = _mapper.Map<TmsFormularioObligacionCaracteristicaValor>(formularioObligacionCaracteristicaValorRequest);
                formularioObligacionCaracteristicaValor.FechaResgitro = DateTime.Now;
                formularioObligacionCaracteristicaValor.UsuarioRegistro = "admin@mail.com";
                formularioObligacionCaracteristicaValor.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioObligacionCaracteristicaValorService.AddAsync(formularioObligacionCaracteristicaValor);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionCaracteristicaValorResponse>(formularioObligacionCaracteristicaValor)
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

        // POST: api/FormularioObligacionCaracteristicaValor/lista
        [HttpPost("lista")]
        public async Task<ActionResult<GenericResponse>> CreateLista(List<FormularioObligacionCaracteristicaValorRequest> formularioObligacionCaracteristicaValorListRequest)
        {
            try
            {
                foreach (FormularioObligacionCaracteristicaValorRequest formularioObligacionCaracteristicaValorRequest in formularioObligacionCaracteristicaValorListRequest)
                {
                    TmsFormularioObligacionCaracteristicaValor formularioObligacionCaracteristicaValor = new TmsFormularioObligacionCaracteristicaValor();
                    formularioObligacionCaracteristicaValor = _mapper.Map<TmsFormularioObligacionCaracteristicaValor>(formularioObligacionCaracteristicaValorRequest);
                    formularioObligacionCaracteristicaValor.FechaResgitro = DateTime.Now;
                    formularioObligacionCaracteristicaValor.UsuarioRegistro = "admin@mail.com";
                    formularioObligacionCaracteristicaValor.Estado = EstadoEnum.ACTIVO.ToString();

                    bool isSaved = await _formularioObligacionCaracteristicaValorService.AddAsync(formularioObligacionCaracteristicaValor);
                    if (!isSaved)
                    {
                        return BadRequest();
                    }
                }
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = "Todos guardados"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        // DELETE: api/FormularioObligacionCaracteristicaValor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formularioObligacionCaracteristicaValor = await _formularioObligacionCaracteristicaValorService.GetById(id);

            if (formularioObligacionCaracteristicaValor == null)
            {
                return NotFound();
            }

            await _formularioObligacionCaracteristicaValorService.DeleteAsync(formularioObligacionCaracteristicaValor);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/FormularioObligacionCaracteristicaValor/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FormularioObligacionCaracteristicaValorRequest formularioObligacionCaracteristicaValorRequest)
        {
            try
            {
                var formularioObligacionCaracteristicaValorActual = await _formularioObligacionCaracteristicaValorService.GetById(id);
                if (formularioObligacionCaracteristicaValorActual == null) { return NotFound(); }

                TmsFormularioObligacionCaracteristicaValor formularioObligacionCaracteristicaValor = new TmsFormularioObligacionCaracteristicaValor();
                formularioObligacionCaracteristicaValor = _mapper.Map<TmsFormularioObligacionCaracteristicaValor>(formularioObligacionCaracteristicaValorRequest);

                formularioObligacionCaracteristicaValor.IdFormularioObligacionCaracteristicaValor = id;
                formularioObligacionCaracteristicaValor.FechaResgitro = formularioObligacionCaracteristicaValorActual.FechaResgitro;
                formularioObligacionCaracteristicaValor.UsuarioRegistro = formularioObligacionCaracteristicaValorActual.UsuarioRegistro;
                formularioObligacionCaracteristicaValor.Estado = formularioObligacionCaracteristicaValorActual.Estado;
                formularioObligacionCaracteristicaValor.FechaModificacion = DateTime.Now;
                formularioObligacionCaracteristicaValor.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _formularioObligacionCaracteristicaValorService.UpdateAsync(formularioObligacionCaracteristicaValor);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioObligacionCaracteristicaValorResponse>(formularioObligacionCaracteristicaValor)
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
