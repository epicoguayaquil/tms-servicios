using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class ActividadEconomicaRepository : CommonRepository<TmsActividadEconomica>, IActividadEconomicaRepository
    {
        public ActividadEconomicaRepository(TmsDbContext dbContext) : base(dbContext)
        {

        }
    
    }
}
