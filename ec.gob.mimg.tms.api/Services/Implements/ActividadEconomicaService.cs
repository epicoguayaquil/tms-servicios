using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class ActividadEconomicaService : CommonManager<TmsActividadEconomica>, IActividadEconomicaService
    {
        public ActividadEconomicaService(TmsDbContext _dbContext) : base(new ActividadEconomicaRepository(_dbContext))
        {
           
        }

        public async Task<ICollection<TmsActividadEconomica>> GetByNivelAsync(int nivel)
        {
            return await GetAsync(x=> x.Nivel==nivel);
        }

        public async Task<TmsActividadEconomica> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdActividadEconomica == id);
        }

        public async Task<TmsActividadEconomica> GetByCodigo(string codigo)
        {
            return await GetFirstOrDefaultAsync(x => x.Codigo == codigo);
        }
    }
}
