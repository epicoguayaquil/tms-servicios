using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IRegistroNotificacionService : ICommonManager<TmsNotificacion>
    {
        Task<TmsNotificacion> GetById(int id);

        Task<ICollection<TmsNotificacion>> GetListByJerarquiaAndObjetoId(string jerarquia, int objetoId);

    }
}
