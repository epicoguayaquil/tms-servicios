﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Services.Implements;
using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using ec.gob.mimg.tms.api.DTOs;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioDetalleController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly FormularioDetalleService _formularioDetalleService;

        private readonly IMapper _mapper;

        public FormularioDetalleController(IMapper mapper, TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _formularioDetalleService = new FormularioDetalleService(_dbContext);
        }

        // GET: api/FormularioDetalle
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var formularioList = await _formularioDetalleService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioList.Select(x => _mapper.Map<FormularioDetalleResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/FormularioDetalle/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formularioDetalle = await _formularioDetalleService.GetFirstOrDefaultAsync(x => x.IdFormularioDetalle == id);

            if (formularioDetalle == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<FormularioDetalleResponse>(formularioDetalle)
                };
                return Ok(response);
            }
        }

        // POST: api/FormularioDetalle
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioDetalleRequest formularioDetalleRequest)
        {
            try
            {
                TmsFormularioDetalle formularioDetalle = new TmsFormularioDetalle();
                formularioDetalle = _mapper.Map<TmsFormularioDetalle>(formularioDetalleRequest);
                formularioDetalle.FechaRegistro = DateTime.Now;
                formularioDetalle.UsuarioRegistro = "admin@mail.com";
                //formularioDetalle.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioDetalleService.AddAsync(formularioDetalle);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioDetalleResponse>(formularioDetalle)
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

        // DELETE: api/FormularioDetalle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formularioDetalle = await _formularioDetalleService.GetFirstOrDefaultAsync(x => x.IdFormularioDetalle == id);

            if (formularioDetalle == null)
            {
                return NotFound();
            }

            await _formularioDetalleService.DeleteAsync(formularioDetalle);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/FormularioDetalle
        [HttpPut]
        public async Task<IActionResult> Update(FormularioDetalleRequest formularioDetalleRequest)
        {
            try
            {
                var formularioDetalleActual = await _formularioDetalleService.GetFirstOrDefaultAsync(x => x.IdFormularioDetalle == formularioDetalleRequest.IdFormularioDetalle);
                if (formularioDetalleActual == null) { return NotFound(); }

                TmsFormularioDetalle formularioDetalle = new TmsFormularioDetalle();
                formularioDetalle = _mapper.Map<TmsFormularioDetalle>(formularioDetalleRequest);

                formularioDetalle.FechaRegistro = formularioDetalleActual.FechaRegistro;
                formularioDetalle.UsuarioRegistro = formularioDetalleActual.UsuarioRegistro;
                //formularioDetalle.Estado = formularioDetalleActual.Estado;
                formularioDetalle.FechaModificacion = DateTime.Now;
                formularioDetalle.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _formularioDetalleService.UpdateAsync(formularioDetalle);
                    
                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioDetalleResponse>(formularioDetalle)
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