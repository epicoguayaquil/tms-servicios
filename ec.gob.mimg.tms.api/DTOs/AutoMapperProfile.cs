using AutoMapper;
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
            CreateMap<TmsEstablecimiento, EstablecimientoResponse>();
            CreateMap<EstablecimientoResponse, TmsEstablecimiento>();


        }
    }
}
