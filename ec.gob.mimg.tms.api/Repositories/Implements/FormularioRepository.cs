using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class FormularioRepository : CommonRepository<TmsFormulario>, IFormularioRepository
    {
        public FormularioRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
