using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IFormularioDetalleService : ICommonManager<TmsFormularioDetalle>
    {
        Task<TmsFormularioDetalle> GetById(int id);

        Task<ICollection<TmsFormularioDetalle>> GetListByFormularioId(int formularioId);

        Task<ICollection<TmsFormularioDetalle>> GetListByFormularioIdAndPasoCreacion(int formularioId, int pasoCreacion);

        Task<TmsFormularioDetalle> GetByFormularioIdAndCaracteristica(int formularioId, string caracteristica);
    }
}
