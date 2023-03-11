using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class RegistroNotificacionRepository : CommonRepository<TmsNotificacion>, IRegistroNotificacionRepository
    {
        public RegistroNotificacionRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
