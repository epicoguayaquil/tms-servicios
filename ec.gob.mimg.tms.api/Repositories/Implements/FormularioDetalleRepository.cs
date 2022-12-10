using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class FormularioDetalleRepository : CommonRepository<FormularioDetalle>, IFormularioDetalleRepository
    {
        public FormularioDetalleRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
