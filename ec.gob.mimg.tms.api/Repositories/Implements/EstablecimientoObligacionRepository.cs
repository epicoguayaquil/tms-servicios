using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class EstablecimientoObligacionRepository : CommonRepository<TmsEstablecimientoObligacion>, IEstablecimientoObligacionRepository
    {
        public EstablecimientoObligacionRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
