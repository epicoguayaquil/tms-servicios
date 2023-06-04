using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IFormularioObligacionService : ICommonManager<TmsFormularioObligacion>
    {
        Task<TmsFormularioObligacion> GetById(int id);

        Task<ICollection<TmsFormularioObligacion>> GetListByFormularioId(int formularioId);

        Task<TmsFormularioObligacion> GetByFormularioIdAndObligacionId(int formularioId, int obligacionId);

    }
}
