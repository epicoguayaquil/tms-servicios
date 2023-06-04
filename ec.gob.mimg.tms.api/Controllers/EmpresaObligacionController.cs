using Microsoft.AspNetCore.Mvc;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Services.Implements;
using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.Services;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaObligacionController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IEmpresaObligacionService _empresaObligacionService;

        private readonly IMapper _mapper;

        public EmpresaObligacionController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _empresaObligacionService = new EmpresaObligacionService(_dbContext);
        }

        // GET: api/EmpresaObligacion
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var empresaObligacionList = await _empresaObligacionService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = empresaObligacionList.Select(x => _mapper.Map<EmpresaObligacionResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/EmpresaObligacion/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var empresaObligacion = await _empresaObligacionService.GetById(id);

            if (empresaObligacion == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EmpresaObligacionResponse>(empresaObligacion)
                };
                return Ok(response);
            }
        }

        // POST: api/EmpresaObligacion
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(EmpresaObligacionRequest empresaObligacionRequest)
        {
            try
            {
                TmsEmpresaObligacion empresaObligacion = new TmsEmpresaObligacion();
                empresaObligacion = _mapper.Map<TmsEmpresaObligacion>(empresaObligacionRequest);
                empresaObligacion.FechaRegistro = DateTime.Now;
                empresaObligacion.UsuarioRegistro = "admin@mail.com";
                //establecimientoObligacion.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _empresaObligacionService.AddAsync(empresaObligacion);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EmpresaObligacionResponse>(empresaObligacion)
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

        // DELETE: api/EmpresaObligacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empresaObligacion = await _empresaObligacionService.GetById(id);

            if (empresaObligacion == null)
            {
                return NotFound();
            }

            await _empresaObligacionService.DeleteAsync(empresaObligacion);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/EmpresaObligacion/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmpresaObligacionRequest empresaObligacionRequest)
        {
            try
            {
                var empresaObligacionActual = await _empresaObligacionService.GetById(id);
                if (empresaObligacionActual == null) { return NotFound(); }

                empresaObligacionActual.ObligacionId = empresaObligacionRequest.ObligacionId;
                empresaObligacionActual.EmpresaId = empresaObligacionRequest.EmpresaId;
                empresaObligacionActual.Observacion = empresaObligacionRequest.Observacion;
                empresaObligacionActual.FechaExigibilidad = empresaObligacionRequest.FechaExigibilidad;
                empresaObligacionActual.FechaRenovacion = empresaObligacionRequest.FechaRenovacion;
                empresaObligacionActual.FechaModificacion = DateTime.Now;
                empresaObligacionActual.UsuarioModificacion = "admin@mail.com";

                bool isUpdate = await _empresaObligacionService.UpdateAsync(empresaObligacionActual);
                    
                if (isUpdate)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EmpresaObligacionResponse>(empresaObligacionActual)
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
