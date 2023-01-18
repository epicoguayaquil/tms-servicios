using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IObligacionActividadService : ICommonManager<TmsActividadObligacion>
    {
        Task<TmsActividadObligacion> GetById(int id);

        Task<ICollection<TmsActividadObligacion>> GetListByObligacionId(int obligacionId);

        Task<ICollection<TmsActividadObligacion>> GetListByActividadId(int actividadId);
    }
}
