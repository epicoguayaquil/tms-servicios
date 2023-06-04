using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class ObligacionRepository : CommonRepository<TmsObligacion>, IObligacionRepository
    {
        public ObligacionRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
