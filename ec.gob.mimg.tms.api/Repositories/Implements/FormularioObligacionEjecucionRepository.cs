using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class FormularioObligacionEjecucionRepository : CommonRepository<TmsFormularioObligacionEjecucion>, IFormularioObligacionEjecucionRepository
    {
        public FormularioObligacionEjecucionRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
