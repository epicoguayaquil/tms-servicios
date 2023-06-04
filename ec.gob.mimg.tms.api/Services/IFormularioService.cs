using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IFormularioService : ICommonManager<TmsFormulario>
    {
        Task<TmsFormulario> GetById(int id);

        Task<ICollection<TmsFormulario>> GetListByEstablecimientoId(int establecimientoId);

        Task<ICollection<TmsFormulario>> GetListByEstablecimientoIdAndEstado(int establecimientoId, string estado);

        Task<ICollection<TmsFormulario>> GetListByEstado(string estado);
    }
}
