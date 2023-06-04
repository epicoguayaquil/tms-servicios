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
    public class ObligacionDependenciaController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IObligacionDependenciaService _obligacionDependenciaService;

        private readonly IMapper _mapper;

        public ObligacionDependenciaController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _obligacionDependenciaService = new ObligacionDependenciaService(_dbContext);
        }

        // GET: api/ObligacionDependencia
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var dependenciaList = await _obligacionDependenciaService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = dependenciaList.Select(x => _mapper.Map<ObligacionDependenciaResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/ObligacionDependencia/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var dependencia = await _obligacionDependenciaService.GetById(id);

            if (dependencia == null)
            {
                return NotFound();
            }
            else
            {
                ObligacionDependenciaResponse dependenciaResponse = _mapper.Map<ObligacionDependenciaResponse>(dependencia);
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = dependenciaResponse
                };
                return Ok(response);
            }
        }

        // POST: api/ObligacionDependencia
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(ObligacionDependenciaRequest dependenciaRequest)
        {
            try
            {
                TmsObligacionDependencia dependencia = new TmsObligacionDependencia();
                dependencia = _mapper.Map<TmsObligacionDependencia>(dependenciaRequest);
                dependencia.FechaRegistro = DateTime.Now;
                dependencia.UsuarioRegistro = "admin@mail.com";

                bool isSaved = await _obligacionDependenciaService.AddAsync(dependencia);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<ObligacionDependenciaResponse>(dependencia)
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

        // DELETE: api/ObligacionDependencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dependencia = await _obligacionDependenciaService.GetById(id);

            if (dependencia == null)
            {
                return NotFound();
            }

            await _obligacionDependenciaService.DeleteAsync(dependencia);

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
