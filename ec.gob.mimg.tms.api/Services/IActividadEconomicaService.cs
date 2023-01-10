using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IActividadEconomicaService : ICommonManager<TmsActividadEconomica>
    {
        Task<ICollection<TmsActividadEconomica>> GetByNivelAsync(int nivel);

        Task<TmsActividadEconomica> GetById(int id);

        Task<TmsActividadEconomica> GetByCodigo(string codigo);
    }
}
