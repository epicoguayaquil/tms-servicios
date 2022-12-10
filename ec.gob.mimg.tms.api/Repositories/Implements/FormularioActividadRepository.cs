using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class FormularioActividadRepository : CommonRepository<TmsFormularioActividad>, IFormularioActividadRepository
    {
        public FormularioActividadRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
