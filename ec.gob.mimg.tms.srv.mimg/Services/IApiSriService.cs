using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ec.gob.mimg.tms.srv.mimg.Services
{
    public interface IApiSriService
    {
        Task<ContribuyenteApiResponse> GetContribuyente(ContribuyenteApiRequest request);

        Task<EstablecimientoApiResponse> GetEstablecimientos(EstablecimientoApiRequest request);

        Task<ActividadApiResponse> GetActividadEstablecimiento(ActividadApiRequest request);
        
        Task<EstablecimientoApiResponse> GetEstablecimientosNuevos(string Fecha);

    }

}
