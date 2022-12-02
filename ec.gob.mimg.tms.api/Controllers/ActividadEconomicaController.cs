using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ec.gob.mimg.tms.api.Data;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Services.Implements;
using ec.gob.mimg.tms.api.Services;
using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using ec.gob.mimg.tms.api.DTOs;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadEconomicaController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly ActividadEconomicaService _actividadEconomicaService;

        private readonly IMapper _mapper;

        public ActividadEconomicaController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _actividadEconomicaService = new ActividadEconomicaService(_dbContext);
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var actividadList = _actividadEconomicaService.GetByNivelAsync(7);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = actividadList.Select(x => _mapper.Map<ActividadEconomicaResponse>(x))
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var actividad = await _actividadEconomicaService.GetFirstOrDefaultAsync(x => x.IdActividadEconomica == id);

            if (actividad == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<ActividadEconomicaResponse>(actividad)
                };
                return Ok(response);
            }
        }

        [HttpGet("[action]/{codigo}")]
        public async Task<ActionResult<GenericResponse>> ByCodigo(string codigo)
        {
            var actividad = await _actividadEconomicaService.GetFirstOrDefaultAsync(x => x.Codigo == codigo);

            if (actividad == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<ActividadEconomicaResponse>(actividad)
                };
                return Ok(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var actividad = await _actividadEconomicaService.GetFirstOrDefaultAsync(x => x.IdActividadEconomica == id);

            if (actividad == null)
            {
                return NotFound();
            }

            await _actividadEconomicaService.DeleteAsync(actividad);

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
