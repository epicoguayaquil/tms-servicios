using ec.gob.mimg.tms.api.Data;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class EmpresaRepository : CommonRepository<TmsEmpresa>, IEmpresaRepository
    {
        public EmpresaRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
