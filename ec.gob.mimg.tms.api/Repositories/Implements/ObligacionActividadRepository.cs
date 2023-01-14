using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class ObligacionActividadRepository : CommonRepository<TmsActividadObligacion>, IObligacionActividadRepository
    {
        public ObligacionActividadRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
