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
    public class ObligacionCaracteristicaController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IObligacionCaracteristicaService _obligacionCaracteristicaService;

        private readonly IMapper _mapper;

        public ObligacionCaracteristicaController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _obligacionCaracteristicaService = new ObligacionCaracteristicaService(_dbContext);
        }

        // GET: api/ObligacionCaracteristica
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var obligacionActividadList = await _obligacionCaracteristicaService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = obligacionActividadList.Select(x => _mapper.Map<ObligacionCaracteristicaResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/ObligacionCaracteristica/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var obligacionCaracteristica = await _obligacionCaracteristicaService.GetById(id);

            if (obligacionCaracteristica == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<ObligacionCaracteristicaResponse>(obligacionCaracteristica)
                };
                return Ok(response);
            }
        }

        // POST: api/ObligacionCaracteristica
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(ObligacionCaracteristicaRequest obligacionCaracteristicaRequest)
        {
            try
            {
                TmsObligacionCaracteristica obligacionCaracteristica = new TmsObligacionCaracteristica();
                obligacionCaracteristica = _mapper.Map<TmsObligacionCaracteristica>(obligacionCaracteristicaRequest);
                obligacionCaracteristica.FechaResgitro = DateTime.Now;
                obligacionCaracteristica.UsuarioRegistro = "admin@mail.com";
                obligacionCaracteristica.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _obligacionCaracteristicaService.AddAsync(obligacionCaracteristica);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<ObligacionCaracteristicaResponse>(obligacionCaracteristica)
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

        // POST: api/ObligacionCaracteristica/lista
        [HttpPost("lista")]
        public async Task<ActionResult<GenericResponse>> CreateLista(List<ObligacionCaracteristicaRequest> obligacionCaracteristicaListRequest)
        {
            try
            {
                foreach (ObligacionCaracteristicaRequest obligacionCaracteristicaRequest in obligacionCaracteristicaListRequest)
                {
                    TmsObligacionCaracteristica obligacionCaracteristica = new TmsObligacionCaracteristica();
                    obligacionCaracteristica = _mapper.Map<TmsObligacionCaracteristica>(obligacionCaracteristicaRequest);
                    obligacionCaracteristica.FechaResgitro = DateTime.Now;
                    obligacionCaracteristica.UsuarioRegistro = "admin@mail.com";
                    obligacionCaracteristica.Estado = EstadoEnum.ACTIVO.ToString();

                    bool isSaved = await _obligacionCaracteristicaService.AddAsync(obligacionCaracteristica);
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

        // DELETE: api/ObligacionCaracteristica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obligacionCaracteristica = await _obligacionCaracteristicaService.GetById(id);

            if (obligacionCaracteristica == null)
            {
                return NotFound();
            }

            await _obligacionCaracteristicaService.DeleteAsync(obligacionCaracteristica);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/ObligacionCaracteristica/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ObligacionCaracteristicaRequest obligacionCaracteristicaRequest)
        {
            try
            {
                var obligacionCaracteristicaActual = await _obligacionCaracteristicaService.GetById(id);
                if (obligacionCaracteristicaActual == null) { return NotFound(); }

                obligacionCaracteristicaActual.FechaModificacion = DateTime.Now;
                obligacionCaracteristicaActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _obligacionCaracteristicaService.UpdateAsync(obligacionCaracteristicaActual);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<ObligacionCaracteristicaResponse>(obligacionCaracteristicaActual)
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
