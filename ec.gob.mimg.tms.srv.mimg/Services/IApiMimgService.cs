using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ec.gob.mimg.tms.srv.mimg.Services
{
    public interface IApiMimgService
    {

        Task<PredioApiResponse> GetCatalogoActividades(PredioApiRequest request);

        Task<FactibilidadUsoResponse> GetFacilidadUso(FactibilidadUsoRequest request);

        Task<DimensionesResponse> GetDimensionMinima(DimensionesRequest request);

        Task<PredioApiResponse> CreateTasaHabilitacion(PredioApiRequest request);

    }
}
