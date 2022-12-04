using AutoMapper;
using ec.gob.mimg.tms.api.Services.Implements;
using ec.gob.mimg.tms.srv.sri.DTOs;
using ec.gob.mimg.tms.srv.sri.Models;
using Microsoft.AspNetCore.Mvc;

namespace ec.gob.mimg.tms.srv.sri.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaDataController : ControllerBase
    {
        private readonly DatecDbContext _dbContext;
        private readonly EmpresaDataService _service;

        private readonly IMapper _mapper;

        public EmpresaDataController(IMapper mapper, DatecDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _service = new EmpresaDataService(_dbContext);
        }


        [HttpGet]
        public async Task<ActionResult<EmpresaData>> GetAll()
        {

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK"
            };

            var empresaList = await _service.GetAllAsync();
            response.Data = empresaList;
            return Ok(response);
        }

        [HttpGet("byRuc/{ruc}")]
        public async Task<ActionResult<GenericResponse>> GetByRuc(string ruc)
        {
            var empresaData = await _service.GetByRucAsync(ruc);

            if (empresaData == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = empresaData
                };
                return Ok(response);
            }
        }

    }
}
