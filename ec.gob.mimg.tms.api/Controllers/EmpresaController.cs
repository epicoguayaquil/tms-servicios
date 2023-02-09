﻿using AutoMapper;
using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using ec.gob.mimg.tms.api.Services;
using ec.gob.mimg.tms.api.Services.Implements;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mail.Services;
using Microsoft.AspNetCore.Mvc;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TmsDbContext _dbContext;
        private readonly ILogger<EmpresaController> _logger;

        private readonly IEmpresaService _empresaService;
        private readonly INotificacionService _notificacionService;
        private readonly IEstablecimientoService _establecimientoService;
        private readonly IEmpresaObligacionService _empresaObligacionService;
        private readonly IObligacionService _obligacionService;

        public EmpresaController(IMapper mapper, TmsDbContext dbContext,
                                ILogger<EmpresaController> logger,
                                INotificacionService notificacionService)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
            _empresaService = new EmpresaService(_dbContext);
            _establecimientoService = new EstablecimientoService(_dbContext);
            _notificacionService = notificacionService;
            _empresaObligacionService = new EmpresaObligacionService(_dbContext);
            _obligacionService = new ObligacionService(_dbContext);
        }

        // GET: api/Empresas
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            _logger.LogError(">>> Consultando Empresas ..............................");
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
            };

            var empresaList = await _empresaService.GetAllAsync();
            response.Data = empresaList.Select(x => _mapper.Map<EmpresaResponse>(x));

            return Ok(response);
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var empresa = await _empresaService.GetById(id);

            if (empresa == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EmpresaResponse>(empresa)
                };
                return Ok(response);
            }
        }

        // GET: api/Empresas/byRuc/5
        [HttpGet("byRuc/{ruc}")]
        public async Task<ActionResult<GenericResponse>> GetByRuc(string ruc)
        {
            var empresa = await _empresaService.GetByRucAsync(ruc);

            if (empresa == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<EmpresaResponse>(empresa)
                };
                return Ok(response);
            }
        }

        // GET: api/Empresas/5/establecimientos
        [HttpGet("{id}/establecimientos")]
        public async Task<ActionResult<GenericResponse>> GetEstablecimientosById(int id)
        {
            var establecimientoList = await _establecimientoService.GetAsync(x => x.EmpresaId == id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = establecimientoList.Select(x => _mapper.Map<EstablecimientoResponse>(x))
            };
            return Ok(response);
        }

        // POST: api/Empresas
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(EmpresaRequest empresaRequest)
        {
            try
            {
                TmsEmpresa empresa = new TmsEmpresa();
                empresa = _mapper.Map<TmsEmpresa>(empresaRequest);
                empresa.FechaRegistro = DateTime.Now;
                empresa.UsuarioRegistro = "admin@mail.com";
                empresa.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _empresaService.AddAsync(empresa);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EmpresaResponse>(empresa)
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

        // DELETE: api/TmsEmpresas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _empresaService.GetById(id);

            if (empresa == null)
            {
                return NotFound();
            }

            await _empresaService.DeleteAsync(empresa);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/TmsEmpresas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Update(EmpresaRequest empresaRequest)
        {
            try
            {
                var empresaActual = await _empresaService.GetByRucAsync(empresaRequest.Ruc);

                if (empresaActual == null) { return NotFound(); }

                TmsEmpresa empresa = _mapper.Map<TmsEmpresa>(empresaRequest);
                empresa.IdEmpresa = empresaActual.IdEmpresa;

                empresa.FechaRegistro = empresaActual.FechaRegistro;
                empresa.UsuarioRegistro = empresaActual.UsuarioRegistro;
                empresa.Estado = empresaActual.Estado;

                empresa.FechaModificacion = DateTime.Now;
                empresa.UsuarioModificacion = "admin@mail.com";


                bool isSaved = await _empresaService.UpdateAsync(empresa);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<EmpresaResponse>(empresa)
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

        // GET: api/Empresa/1/obligaciones
        [HttpGet("{id}/obligaciones")]
        public async Task<ActionResult<GenericResponse>> GetAllObligacionesById(int id)
        {
            var empresaObligacionList = await _empresaObligacionService.GetListByEmpresaId(id);
            var empresaObligacionResponseListNew = new List<EmpresaObligacionResponse>();
            foreach (var empresaObligacion in empresaObligacionList)
            {
                var empresaObligacionResponse = _mapper.Map<EmpresaObligacionResponse>(empresaObligacion);
                var obligacion = await _obligacionService.GetById(empresaObligacion.ObligacionId);
                empresaObligacionResponse.Obligacion = _mapper.Map<ObligacionResponse>(obligacion);
                empresaObligacionResponseListNew.Add(empresaObligacionResponse);
            }
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = empresaObligacionResponseListNew
            };

            return Ok(response);
        }

        // POST: api/Empresa/1/obligacionesGenerales
        [HttpPost("{id}/obligacionesGenerales")]
        public async Task<ActionResult<GenericResponse>> GenerarObligacionesGenerales(int id)
        {
            var obligacionesList = await _obligacionService.GetListByJerarquia(JerarquiaObligacion.EMPRESA.ToString());
            int contadorGeneradas = 0;
            foreach (var obligacion in obligacionesList)
            {
                TmsEmpresaObligacion empresaObligacion;
                empresaObligacion = await _empresaObligacionService.GetByEmpresaIdAndObligacionId(id, obligacion.IdObligacion);
                if (empresaObligacion == null)
                {
                    empresaObligacion = new()
                    {
                        EmpresaId = id,
                        ObligacionId = obligacion.IdObligacion,
                        Observacion = "",
                        FechaExigibilidad = DateTime.Now,
                        FechaRenovacion = DateTime.Now,
                        Estado = EstadoObligacionEnum.NO_CUMPLE.ToString(),
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "admin@mail.com"
                    };
                    bool isSaved = await _empresaObligacionService.AddAsync(empresaObligacion);
                    if (isSaved)
                    {
                        contadorGeneradas++;
                    }
                }
            }
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Obligaciones generadas: " + contadorGeneradas
            };

            return Ok(response);
        }

    }
}
