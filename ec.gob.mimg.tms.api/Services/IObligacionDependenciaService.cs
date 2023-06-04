using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IObligacionDependenciaService : ICommonManager<TmsObligacionDependencia>
    {
        Task<TmsObligacionDependencia> GetById(int id);

        Task<ICollection<TmsObligacionDependencia>> GetListaPadresByIdHijo(int hijoId);

        Task<ICollection<TmsObligacionDependencia>> GetListaHijosByIdPadre(int padreId);
    }
}
