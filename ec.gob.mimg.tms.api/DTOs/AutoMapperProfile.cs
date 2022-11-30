using AutoMapper;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.Models;

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
        }
    }
}
