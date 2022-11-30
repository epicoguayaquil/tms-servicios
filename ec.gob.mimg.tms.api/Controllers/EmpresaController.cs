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
using ec.gob.mimg.tms.api.Enums;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly EmpresaService _empresaService;

        private readonly IMapper _mapper;

        public EmpresaController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _empresaService = new EmpresaService(_dbContext);
        }

        // GET: api/Empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaResponse>>> GetAll()
        {
            var empresaList = await _empresaService.GetAllAsync();
            return Ok(empresaList.Select(x => _mapper.Map<EmpresaResponse>(x)));
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaResponse>> GetById(int id)
        {
            var empresa = await _empresaService.GetFirstOrDefaultAsync(x => x.IdEmpresa == id);

            if (empresa == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<EmpresaResponse>(empresa));
            }
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


                bool isSaved = await _empresaService.UpdateAsync(empresa)
                    
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
