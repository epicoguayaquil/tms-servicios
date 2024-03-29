﻿using Microsoft.AspNetCore.Mvc;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Services.Implements;
using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using ec.gob.mimg.tms.api.DTOs;
using ec.gob.mimg.tms.api.Services;
using Microsoft.IdentityModel.Tokens;
using ec.gob.mimg.tms.srv.mimg.Services;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace ec.gob.mimg.tms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioController : ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        private readonly IEstablecimientoService _establecimientoService;
        private readonly IFormularioService _formularioService;
        private readonly IFormularioDetalleService _formularioDetalleService;
        private readonly IFormularioActividadService _formularioActividadService;
        private readonly IFormularioObligacionService _formularioObligacionService;
        private readonly IObligacionActividadService _obligacionActividadService;
        private readonly IObligacionService _obligacionService;
        private readonly IApiCatastroService _apiCatastroService;
        private readonly IObligacionCaracteristicaService _obligacionCaracteristicaService;
        private readonly IRegistroNotificacionService _registroNotificacionService;
        private readonly IObligacionDependenciaService _obligacionDependenciaService;

        private readonly IMapper _mapper;

        public FormularioController(IMapper mapper, TmsDbContext dbContext,
            IApiCatastroService apiCatastroService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _establecimientoService = new EstablecimientoService(_dbContext);
            _formularioService = new FormularioService(_dbContext);
            _formularioDetalleService = new FormularioDetalleService(_dbContext);
            _formularioActividadService = new FormularioActividadService(_dbContext);
            _formularioObligacionService = new FormularioObligacionService(_dbContext);
            _obligacionActividadService = new ObligacionActividadService(_dbContext);
            _obligacionService = new ObligacionService(_dbContext);
            _apiCatastroService = apiCatastroService;
            _obligacionCaracteristicaService = new ObligacionCaracteristicaService(_dbContext);
            _registroNotificacionService = new RegistroNotificacionService(_dbContext);
            _obligacionDependenciaService = new ObligacionDependenciaService(_dbContext);
        }

        // GET: api/Formulario
        [HttpGet]
        public async Task<ActionResult<GenericResponse>> GetAll()
        {
            var formularioList = await _formularioService.GetAllAsync();
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioList.Select(x => _mapper.Map<FormularioResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Formulario/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse>> GetById(int id)
        {
            var formulario = await _formularioService.GetById(id);

            if (formulario == null)
            {
                return NotFound();
            }
            else
            {
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = _mapper.Map<FormularioResponse>(formulario)
                };
                return Ok(response);
            }
        }

        // GET: api/Formulario/1/detalles
        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<GenericResponse>> GetAllDetallesById(int id)
        {
            var formularioDetalleList = await _formularioDetalleService.GetListByFormularioId(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioDetalleList.Select(x => _mapper.Map<FormularioDetalleResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Formulario/1/detallesNivel/2
        [HttpGet("{id}/detallesNivel/{pasoCreacion}")]
        public async Task<ActionResult<GenericResponse>> GetAllDetallesNivelById(int id, int pasoCreacion)
        {
            var formularioDetalleList = await _formularioDetalleService.GetListByFormularioIdAndPasoCreacion(id, pasoCreacion);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioDetalleList.Select(x => _mapper.Map<FormularioDetalleResponse>(x))
            };

            return Ok(response);
        }

        // GET: api/Formulario/1/actividades
        [HttpGet("{id}/actividades")]
        public async Task<ActionResult<GenericResponse>> GetAllActividadesById(int id)
        {
            var formularioActividadList = await _formularioActividadService.GetListByFormularioId(id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioActividadList.Select(x => _mapper.Map<FormularioActividadResponse>(x))
            };

            return Ok(response);
        }

        // PUT: api/Formulario/1/confirmar
        [HttpPut("{id}/confirmar")]
        public async Task<IActionResult> GenerarObligaciones(int id)
        {
            try
            {
                var formularioActividadList = await _formularioActividadService.GetListByFormularioId(id);
                TmsFormularioActividad firstActividad = formularioActividadList.First();

                var obligacionActividadList = await _obligacionActividadService.GetListByActividadId(firstActividad.ActividadEconomicaId);

                int contadorGeneradas = 0;
                foreach (TmsActividadObligacion actividadObligacion in obligacionActividadList)
                {
                    TmsFormularioObligacion formularioObligacion;
                    formularioObligacion = await _formularioObligacionService.GetByFormularioIdAndObligacionId(id, actividadObligacion.ObligacionId);
                    if (formularioObligacion == null)
                    {
                        var obligacion = await _obligacionService.GetById(actividadObligacion.ObligacionId);
                        formularioObligacion = new()
                        {
                            ObligacionId = actividadObligacion.ObligacionId,
                            FormularioId = id,
                            //FechaExigibilidad = calcularFechaParaObligacion(obligacion.MesExigibilidad),
                            //FechaRenovacion = calcularFechaParaObligacion(obligacion.MesRenovacion),
                            FechaRegistro = DateTime.Now,
                            UsuarioRegistro = "admin@mail.com",
                            Estado = EstadoObligacionEnum.NO_CUMPLE.ToString()
                        };
                        //if (obligacion.TipoExigibilidad == TipoExigibilidadEnum.VENCIMIENTO.ToString())
                        //{
                        //    formularioObligacion.FechaRenovacion = DateTime.Now.AddYears(1);
                        //    formularioObligacion.FechaExigibilidad = formularioObligacion.FechaRenovacion;
                        //}

                        bool isSaved = await _formularioObligacionService.AddAsync(formularioObligacion);
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

                var formulario = await _formularioService.GetById(id);
                bool update = await _establecimientoService.UpdateEstadoRegistroById(formulario.EstablecimientoId, EstadoRegistroEnum.REGISTRADO.ToString());
                if (formulario.Estado == EstadoEnum.EN_PROCESO.ToString())
                {
                    var formularioActivoList = await _formularioService.GetListByEstablecimientoIdAndEstado(formulario.EstablecimientoId, EstadoEnum.ACTIVO.ToString());
                    foreach(var formularioActivo in formularioActivoList)
                    {
                        formularioActivo.Estado = EstadoEnum.INACTIVO.ToString();
                        await _formularioService.UpdateAsync(formularioActivo);
                    }
                    formulario.Estado = EstadoEnum.ACTIVO.ToString();
                    await _formularioService.UpdateAsync(formulario);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        private static DateTime calcularFechaParaObligacion(int? mesPlan)
        {
            var fechEnUnAnio = DateTime.Now.AddYears(1);
            if (mesPlan == null)
            {
                return fechEnUnAnio;
            }
            return new DateTime(fechEnUnAnio.Year, (int)mesPlan, 1); ;
        }

        // GET: api/Formulario/1/obligaciones
        [HttpGet("{id}/obligaciones")]
        public async Task<ActionResult<GenericResponse>> GetAllObligacionesById(int id)
        {
            var formularioObligacionList = await _formularioObligacionService.GetListByFormularioId(id);
            var formularioObligacionResponseListNew = new List<ObjetoObligacionResponse>();
            foreach (var formularioObligacion in formularioObligacionList)
            {
                var formularioObligacionRequest = _mapper.Map<ObjetoObligacionResponse>(formularioObligacion);
                formularioObligacionRequest.IdObjetoObligacion = formularioObligacion.IdFormularioObligacion;
                formularioObligacionRequest.ReferenciaId = formularioObligacion.FormularioId;
                var obligacion = await _obligacionService.GetById(formularioObligacion.ObligacionId);
                formularioObligacionRequest.NombreObligacion = obligacion.Nombre;
                formularioObligacionRequest.OrdenEjecucion = obligacion.OrdenEjecucion;
                var padresList = await _obligacionDependenciaService.GetListaPadresByIdHijo(formularioObligacion.ObligacionId);
                formularioObligacionRequest.Dependencias = padresList.Select(x => _mapper.Map<ObligacionDependenciaResponse>(x));
                
                var obligacionCaracteristicaList = await _obligacionCaracteristicaService.GetListByObligacionIdAndTipo(formularioObligacion.ObligacionId, TipoCaracteristica.GESTION.ToString());
                ProcesarCaracteristicasDeGestion(obligacionCaracteristicaList, formularioObligacionRequest, formularioObligacion.IdFormularioObligacion);

                formularioObligacionResponseListNew.Add(formularioObligacionRequest);
            }
            _obligacionService.ProcesarVariablesDeObligaciones(formularioObligacionResponseListNew);

            formularioObligacionResponseListNew.Sort(delegate (ObjetoObligacionResponse x, ObjetoObligacionResponse y)
            {
                if (x.OrdenEjecucion == y.OrdenEjecucion)
                {
                    return 0;
                } else if (x.OrdenEjecucion > y.OrdenEjecucion)
                {
                    return 1;
                } else
                {
                    return -1;
                }
            });
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = formularioObligacionResponseListNew
            };

            return Ok(response);
        }

        private static void ProcesarCaracteristicasDeGestion(ICollection<TmsObligacionCaracteristica>? obligacionCaracteristicaList,
            ObjetoObligacionResponse formularioObligacionRequest, int idFormularioObligacion)
        {
            if (obligacionCaracteristicaList == null)
            {
                return;
            }
            foreach (var obligacionCaracteristica in obligacionCaracteristicaList)
            {
                if (obligacionCaracteristica.Nombre == "tipo_generacion")
                {
                    formularioObligacionRequest.TipoGeneracion = obligacionCaracteristica.Valor;
                }
                else if (obligacionCaracteristica.Nombre == "tiene_formulario")
                {
                    formularioObligacionRequest.TieneFormulario = (obligacionCaracteristica.Valor == "Si");
                }
                else if (obligacionCaracteristica.Nombre == "permite_excepción")
                {
                    formularioObligacionRequest.PermiteExcepcion = (obligacionCaracteristica.Valor == "Si");
                }
                else if (obligacionCaracteristica.Nombre == "URL_excepcion")
                {
                    if (obligacionCaracteristica.Valor == null)
                    {
                        formularioObligacionRequest.UrlExcepcion = "";
                    }
                    else
                    {
                        formularioObligacionRequest.UrlExcepcion = obligacionCaracteristica.Valor.Replace("{id}", idFormularioObligacion.ToString());
                    }
                }
                else if (obligacionCaracteristica.Nombre == "URL_ejecucion")
                {
                    if (obligacionCaracteristica.Valor == null)
                    {
                        formularioObligacionRequest.UrlEjecucion = "";
                    }
                    else
                    {
                        formularioObligacionRequest.UrlEjecucion = obligacionCaracteristica.Valor.Replace("{id}", idFormularioObligacion.ToString());
                    }
                }
            }
        }


        // POST: api/Formulario
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> Create(FormularioRequest formularioRequest)
        {
            try
            {
                TmsFormulario formulario = new TmsFormulario();
                formulario = _mapper.Map<TmsFormulario>(formularioRequest);
                formulario.FechaRegistro = DateTime.Now;
                formulario.UsuarioRegistro = "admin@mail.com";
                formulario.Estado = EstadoEnum.ACTIVO.ToString();

                bool isSaved = await _formularioService.AddAsync(formulario);

                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioResponse>(formulario)
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

        // POST: api/Formulario/detallesNivel
        [HttpPost("detallesNivel")]
        public async Task<ActionResult<GenericResponse>> CreateDetallesNivel(FormularioDetalleListRequest formularioDetalleListRequest)
        {
            try
            {
                if (formularioDetalleListRequest == null)
                {
                    return BadRequest();
                }
                if (formularioDetalleListRequest.CaracteristicaList.IsNullOrEmpty())
                {
                    return BadRequest();
                }
                foreach (var caracteristicaElement in formularioDetalleListRequest.CaracteristicaList)
                {
                    TmsFormularioDetalle formularioDetalleActual;
                    formularioDetalleActual  = await _formularioDetalleService.
                        GetByFormularioIdAndCaracteristica(formularioDetalleListRequest.FormularioId,
                            caracteristicaElement.Caracteristica);
                    if (formularioDetalleActual == null) {
                        TmsFormularioDetalle formularioDetalle = new()
                        {
                            Caracteristica = caracteristicaElement.Caracteristica,
                            Valor = caracteristicaElement.Valor,
                            TipoDato = caracteristicaElement.TipoDato,
                            ExtraInfo = caracteristicaElement.ExtraInfo,
                            FormularioId = formularioDetalleListRequest.FormularioId,
                            PasoCreacion = formularioDetalleListRequest.PasoCreacion,
                            FechaRegistro = DateTime.Now,
                            UsuarioRegistro = "admin@mail.com"
                        };
                        bool isSaved = await _formularioDetalleService.AddAsync(formularioDetalle);
                    }
                    else
                    {
                        formularioDetalleActual.Valor = caracteristicaElement.Valor;
                        formularioDetalleActual.TipoDato = caracteristicaElement.TipoDato;
                        formularioDetalleActual.ExtraInfo = caracteristicaElement.ExtraInfo;
                        formularioDetalleActual.FechaModificacion = DateTime.Now;
                        formularioDetalleActual.UsuarioModificacion = "admin@mail.com";
                        bool isUpdate = await _formularioDetalleService.UpdateAsync(formularioDetalleActual);
                    }
                }

                var formulario = await _formularioService.GetById(formularioDetalleListRequest.FormularioId);
                bool update = await _establecimientoService.UpdateEstadoRegistroById(formulario.EstablecimientoId, EstadoRegistroEnum.EN_PROCESO.ToString());
                
                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = "All saved"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        // DELETE: api/Formulario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formulario = await _formularioService.GetById(id);

            if (formulario == null)
            {
                return NotFound();
            }

            await _formularioService.DeleteAsync(formulario);

            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = "Eliminado"
            };
            return Ok(response);
        }

        // PUT: api/Formulario/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FormularioRequest formularioRequest)
        {
            try
            {
                var formularioActual = await _formularioService.GetById(id);

                if (formularioActual == null) { return NotFound(); }

                TmsFormulario formulario = new TmsFormulario();
                formulario = _mapper.Map<TmsFormulario>(formularioRequest);

                formulario.IdFormulario = id;
                formulario.FechaRegistro = formularioActual.FechaRegistro;
                formulario.UsuarioRegistro = formularioActual.UsuarioRegistro;
                formulario.Estado = formularioActual.Estado;
                formulario.FechaModificacion = DateTime.Now;
                formulario.UsuarioModificacion = "admin@mail.com";

                bool isSaved = await _formularioService.UpdateAsync(formulario);
                    
                if (isSaved)
                {
                    GenericResponse response = new()
                    {
                        Cod = "200",
                        Msg = "OK",
                        Data = _mapper.Map<FormularioResponse>(formulario)
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

        // POST: api/Formulario/codigoCatastral
        [HttpPost("codigoCatastral")]
        public async Task<ActionResult<GenericResponse>> ValidarCodigoCatastral(FormularioDetalleListRequest formularioDetalleListRequest)
        {
            try
            {
                if (formularioDetalleListRequest == null)
                {
                    return BadRequest();
                }
                if (formularioDetalleListRequest.CaracteristicaList.IsNullOrEmpty())
                {
                    return BadRequest();
                }

                PredioApiRequest request = new PredioApiRequest();

                foreach (var caracteristicaElement in formularioDetalleListRequest.CaracteristicaList)
                {
                    if (caracteristicaElement.Caracteristica == "Sector")
                    {
                        request.IdSector = caracteristicaElement.Valor;
                    }
                    else if (caracteristicaElement.Caracteristica == "Manzana")
                    {
                        request.Manzana = caracteristicaElement.Valor;
                    }
                    else if (caracteristicaElement.Caracteristica == "Lote")
                    {
                        request.Lote = caracteristicaElement.Valor;
                    }
                    else if (caracteristicaElement.Caracteristica == "Division")
                    {
                        request.Division = caracteristicaElement.Valor;
                    }
                    else if (caracteristicaElement.Caracteristica == "Phv")
                    {
                        request.Phv = caracteristicaElement.Valor;
                    }
                    else if (caracteristicaElement.Caracteristica == "Phh")
                    {
                        request.Phh = caracteristicaElement.Valor;
                    }
                    else if (caracteristicaElement.Caracteristica == "Numero")
                    {
                        request.Numero = caracteristicaElement.Valor;
                    }
                }
                var predio = await _apiCatastroService.GetPredio(request);

                GenericResponse response = new()
                {
                    Cod = "200",
                    Msg = "OK",
                    Data = predio
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        // GET: api/Formulario/5/notificaciones
        [HttpGet("{id}/notificaciones")]
        public async Task<ActionResult<GenericResponse>> GetNotificacionesById(int id)
        {
            var notificacionList = await _registroNotificacionService.GetListByJerarquiaAndObjetoId("Formulario", id);
            GenericResponse response = new()
            {
                Cod = "200",
                Msg = "OK",
                Data = notificacionList.Select(x => _mapper.Map<RegistroNotificacionResponse>(x))
            };
            return Ok(response);
        }

    }
}
