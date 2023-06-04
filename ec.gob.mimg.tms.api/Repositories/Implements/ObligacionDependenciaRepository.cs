using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class ObligacionDependenciaRepository : CommonRepository<TmsObligacionDependencia>, IObligacionDependenciaRepository
    {
        public ObligacionDependenciaRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
