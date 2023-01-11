using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IFormularioActividadService : ICommonManager<TmsFormularioActividad>
    {
        Task<TmsFormularioActividad> GetById(int id);

        Task<ICollection<TmsFormularioActividad>> GetListByFormularioId(int formularioId);
    }

}
