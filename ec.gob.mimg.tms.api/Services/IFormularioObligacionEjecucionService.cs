using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IFormularioObligacionEjecucionService : ICommonManager<TmsFormularioObligacionEjecucion>
    {
        Task<TmsFormularioObligacionEjecucion> GetById(int id);

        Task<ICollection<TmsFormularioObligacionEjecucion>> GetListByFormularioObligacionId(int formularioObligacionId);

    }
}
