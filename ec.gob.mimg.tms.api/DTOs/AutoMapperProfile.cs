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
            CreateMap<TmsEmpresa, EmpresaResponse>();
            CreateMap<EmpresaResponse, TmsEmpresa>();
            CreateMap<TmsEmpresa, EmpresaRequest>();
            CreateMap<EmpresaRequest, TmsEmpresa>();
            CreateMap<TmsEstablecimiento, EstablecimientoResponse>();
            CreateMap<EstablecimientoResponse, TmsEstablecimiento>();
        }
    }
}
