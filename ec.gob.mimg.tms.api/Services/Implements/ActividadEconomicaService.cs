using ec.gob.mimg.tms.api.Data;
using ec.gob.mimg.tms.api.Repositories.Implements;
using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Manager;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class ActividadEconomicaService : CommonManager<TmsActividadEconomica>, IActividadEconomicaService
    {
        public ActividadEconomicaService(TmsDbContext _dbContext) : base(new ActividadEconomicaRepository(_dbContext))
        {
           
        }

        public  ICollection<TmsActividadEconomica> GetByNivelAsync(int nivel)
        {
            return Get(x=> x.Nivel==nivel);
        }
    }
}
