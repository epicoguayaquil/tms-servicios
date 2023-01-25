using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class ObligacionCaracteristicaRepository : CommonRepository<TmsObligacionCaracteristica>, IObligacionCaracteristicaRepository
    {
        public ObligacionCaracteristicaRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
