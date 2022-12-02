﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ec.gob.mimg.tms.api.Data;
using ec.gob.mimg.tms.api.Models;
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
    public class EmpresaController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly EmpresaService _empresaService;
        private readonly EstablecimientoService _establecimientoService;

        private readonly IMapper _mapper;

        public EmpresaController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _empresaService = new EmpresaService(_dbContext);
            _establecimientoService = new EstablecimientoService(_dbContext);
        }

        // GET: api/Empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenericResponse>>> GetAll()
        {
            GenericResponse response = new GenericResponse();
            response.Cod = "200";
            response.Msg = "OK";

            var empresaList = await _empresaService.GetAllAsync();
            response.Data = empresaList.Select(x => _mapper.Map<EmpresaResponse>(x));

            return Ok(response);
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            GenericResponse response = new GenericResponse();
            response.Cod = "200";
            response.Msg = "OK";

            var empresa = await _empresaService.GetFirstOrDefaultAsync(x => x.IdEmpresa == id);

            if (empresa == null)
            {
                return NotFound();
            }
            else
            {
                response.Data = _mapper.Map<EmpresaResponse>(empresa);
                return Ok(response);
            }
        }

        // GET: api/Empresas/byRuc/5
        [HttpGet("byRuc/{ruc}")]
        public async Task<ActionResult<EmpresaResponse>> GetByRuc(string ruc)
        {
            var empresa = await _empresaService.GetByRucAsync(ruc);

            if (empresa == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<EmpresaResponse>(empresa));
            }
        }

        // GET: api/Empresas/5/establecimientos
        [HttpGet("{id}/establecimientos")]
        public async Task<ActionResult<IEnumerable<EstablecimientoResponse>>> GetEstablecimientosById(int id)
        {
            var establecimientoList = await _establecimientoService.GetAllAsync(x => x.EmpresaId == id);
            return Ok(establecimientoList.Select(x => _mapper.Map<EstablecimientoResponse>(x)));
        }

        // POST: api/Empresas
        [HttpPost]
        public async Task<ActionResult<EmpresaResponse>> Create(EmpresaRequest empresaRequest)
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
                    return Ok(_mapper.Map<EmpresaResponse>(empresa));
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
            var empresa = await _empresaService.GetFirstOrDefaultAsync(x => x.IdEmpresa == id);

            if (empresa == null)
            {
                return NotFound();
            }

            await _empresaService.DeleteAsync(empresa);

            return Ok("Eliminado");
        }

        // PUT: api/TmsEmpresas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Update(EmpresaRequest empresaRequest)
        {
            try
            {
                TmsEmpresa empresa = new TmsEmpresa();
                empresa = await _empresaService.GetByRucAsync(empresaRequest.Ruc);

                if (empresa == null) { return NotFound(); }

                empresa.FechaModificacion = DateTime.Now;
                empresa.UsuarioModificacion = "admin@mail.com";


                bool isSaved = await _empresaService.UpdateAsync(empresa);
                    
                if (isSaved)
                {
                    return Ok(_mapper.Map<EmpresaResponse>(empresa));
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString);
                return BadRequest();
            }
        }
    }
}
