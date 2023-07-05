using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class NotificacionMotivoFormatoRepository : CommonRepository<TmsNotificacionMotivoFormato>, INotificacionMotivoFormatoRepository
    {
        public NotificacionMotivoFormatoRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }      
    }
}
