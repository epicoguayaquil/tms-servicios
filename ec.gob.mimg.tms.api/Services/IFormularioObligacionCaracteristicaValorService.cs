using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IFormularioObligacionCaracteristicaValorService : ICommonManager<TmsFormularioObligacionCaracteristicaValor>
    {
        Task<TmsFormularioObligacionCaracteristicaValor> GetById(int id);

        Task<ICollection<TmsFormularioObligacionCaracteristicaValor>> GetListByFormularioObligacionId(int formularioObligacionId);

    }
}
