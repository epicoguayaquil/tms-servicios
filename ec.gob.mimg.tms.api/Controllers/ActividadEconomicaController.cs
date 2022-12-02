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

        // GET: api/Actividades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenericResponse>>> GetAll()
        {
            GenericResponse response = new GenericResponse();
            response.Cod = "200";
            response.Msg = "OK";

            var actividadList = _actividadEconomicaService.GetByNivelAsync(7);

            response.Data = actividadList.Select(x => _mapper.Map<ActividadEconomicaResponse>(x));

            return Ok(response);
        
        }
    }
}
