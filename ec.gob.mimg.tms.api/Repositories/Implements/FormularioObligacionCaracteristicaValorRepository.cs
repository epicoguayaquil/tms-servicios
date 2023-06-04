using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class FormularioObligacionCaracteristicaValorRepository : CommonRepository<TmsFormularioObligacionCaracteristicaValor>, IFormularioObligacionCaracteristicaValorRepository
    {
        public FormularioObligacionCaracteristicaValorRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
