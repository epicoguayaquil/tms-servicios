﻿using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Actividad Economica
            CreateMap<TmsActividadEconomica, ActividadEconomicaResponse>();
            CreateMap<ActividadEconomicaResponse, TmsActividadEconomica>();

            // Empresa
            CreateMap<TmsEmpresa, EmpresaResponse>();
            CreateMap<EmpresaResponse, TmsEmpresa>();

            CreateMap<TmsEmpresa, EmpresaRequest>();
            CreateMap<EmpresaRequest, TmsEmpresa>();

            // Establecimiento
            CreateMap<TmsEstablecimiento, EstablecimientoRequest>();
            CreateMap<EstablecimientoRequest, TmsEstablecimiento>();

            CreateMap<TmsEstablecimiento, EstablecimientoResponse>();
            CreateMap<EstablecimientoResponse, TmsEstablecimiento>();

            // Formulario
            CreateMap<TmsFormulario, FormularioResponse>();
            CreateMap<FormularioResponse, TmsFormulario>();

            CreateMap<TmsFormulario, FormularioRequest>();
            CreateMap<FormularioRequest, TmsFormulario>();

            // FormularioActividad
            CreateMap<TmsFormularioActividad, FormularioActividadResponse>();
            CreateMap<FormularioActividadResponse, TmsFormularioActividad>();

            CreateMap<TmsFormularioActividad, FormularioActividadRequest>();
            CreateMap<FormularioActividadRequest, TmsFormularioActividad>();

            // FormularioDetalle
            CreateMap<TmsFormularioDetalle, FormularioDetalleResponse>();
            CreateMap<FormularioDetalleResponse, TmsFormularioDetalle>();

            CreateMap<TmsFormularioDetalle, FormularioDetalleRequest>();
            CreateMap<FormularioDetalleRequest, TmsFormularioDetalle>();

            // Obligacion
            CreateMap<TmsObligacion, ObligacionResponse>();
            CreateMap<ObligacionResponse, TmsObligacion>();

            CreateMap<TmsObligacion, ObligacionRequest>();
            CreateMap<ObligacionRequest, TmsObligacion>();

            // ObligacionActividad
            CreateMap<TmsActividadObligacion, ObligacionActividadResponse>();
            CreateMap<ObligacionActividadResponse, TmsActividadObligacion>();

            CreateMap<TmsActividadObligacion, ObligacionActividadRequest>();
            CreateMap<ObligacionActividadRequest, TmsActividadObligacion>();

            // FormularioObligacion
            CreateMap<TmsFormularioObligacion, FormularioObligacionResponse>();
            CreateMap<FormularioObligacionResponse, TmsFormularioObligacion>();
            CreateMap<ObjetoObligacionResponse, TmsFormularioObligacion>();
            CreateMap<TmsFormularioObligacion, ObjetoObligacionResponse>();

            CreateMap<TmsFormularioObligacion, FormularioObligacionRequest>();
            CreateMap<FormularioObligacionRequest, TmsFormularioObligacion>();

            // ObligacionActividad
            CreateMap<TmsObligacionCaracteristica, ObligacionCaracteristicaResponse>();
            CreateMap<ObligacionCaracteristicaResponse, TmsObligacionCaracteristica>();

            CreateMap<TmsObligacionCaracteristica, ObligacionCaracteristicaRequest>();
            CreateMap<ObligacionCaracteristicaRequest, TmsObligacionCaracteristica>();

            // ObligacionDependencia
            CreateMap<TmsObligacionDependencia, ObligacionDependenciaResponse>();
            CreateMap<ObligacionDependenciaResponse, TmsObligacionDependencia>();

            CreateMap<TmsObligacionDependencia, ObligacionDependenciaRequest>();
            CreateMap<ObligacionDependenciaRequest, TmsObligacionDependencia>();

            // FormularioObligacionCaracteristicaValor
            CreateMap<TmsFormularioObligacionCaracteristicaValor, FormularioObligacionCaracteristicaValorResponse>();
            CreateMap<FormularioObligacionCaracteristicaValorResponse, TmsFormularioObligacionCaracteristicaValor>();

            CreateMap<TmsFormularioObligacionCaracteristicaValor, FormularioObligacionCaracteristicaValorRequest>();
            CreateMap<FormularioObligacionCaracteristicaValorRequest, TmsFormularioObligacionCaracteristicaValor>();

            // EmpresaObligacion
            CreateMap<TmsEmpresaObligacion, EmpresaObligacionResponse>();
            CreateMap<EmpresaObligacionResponse, TmsEmpresaObligacion>();
            CreateMap<ObjetoObligacionResponse, TmsEmpresaObligacion>();
            CreateMap<TmsEmpresaObligacion, ObjetoObligacionResponse>();

            CreateMap<TmsEmpresaObligacion, EmpresaObligacionRequest>();
            CreateMap<EmpresaObligacionRequest, TmsEmpresaObligacion>();

            // Notificacion
            CreateMap<TmsNotificacion, RegistroNotificacionResponse>();
            CreateMap<RegistroNotificacionResponse, TmsNotificacion>();

            CreateMap<TmsNotificacion, RegistroNotificacionRequest>();
            CreateMap<RegistroNotificacionRequest, TmsNotificacion>();

            // FormularioObligacionEjecucion
            CreateMap<TmsFormularioObligacionEjecucion, FormularioObligacionEjecucionResponse>();
            CreateMap<FormularioObligacionEjecucionResponse, TmsFormularioObligacionEjecucion>();

            CreateMap<TmsFormularioObligacionEjecucion, FormularioObligacionEjecucionRequest>();
            CreateMap<FormularioObligacionEjecucionRequest, TmsFormularioObligacionEjecucion>();

            // NotificacionMotivoFormato
            CreateMap<TmsNotificacionMotivoFormato, NotificacionMotivoFormatoResponse>();
            CreateMap<NotificacionMotivoFormatoResponse, TmsNotificacionMotivoFormato>();

            CreateMap<TmsNotificacionMotivoFormato, NotificacionMotivoFormatoRequest>();
            CreateMap<NotificacionMotivoFormatoRequest, TmsNotificacionMotivoFormato>();
        }
    }
}
