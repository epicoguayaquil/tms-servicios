using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IObligacionCaracteristicaService : ICommonManager<TmsObligacionCaracteristica>
    {
        Task<TmsObligacionCaracteristica> GetById(int id);

        Task<ICollection<TmsObligacionCaracteristica>> GetListByObligacionId(int obligacionId);

        Task<ICollection<TmsObligacionCaracteristica>> GetListByObligacionIdAndTipo(int obligacionId, string tipo);
    }
}
