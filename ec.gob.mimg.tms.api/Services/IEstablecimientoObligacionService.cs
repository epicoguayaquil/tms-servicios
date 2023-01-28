using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IEstablecimientoObligacionService : ICommonManager<TmsEstablecimientoObligacion>
    {
        Task<TmsEstablecimientoObligacion> GetById(int id);

        Task<ICollection<TmsEstablecimientoObligacion>> GetListByEstablecimientoId(int establecimientoId);

        Task<TmsEstablecimientoObligacion> GetByEstablecimientoIdAndObligacionId(int establecimientoId, int obligacionId);

    }
}
