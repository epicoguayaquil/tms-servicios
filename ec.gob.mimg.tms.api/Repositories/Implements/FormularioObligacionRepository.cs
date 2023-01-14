using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class FormularioObligacionRepository : CommonRepository<TmsFormularioObligacion>, IFormularioObligacionRepository
    {
        public FormularioObligacionRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
