using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Repository;
using ec.gob.mimg.tms.srv.sri.Models;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class EmpresaDataRepository : CommonRepository<EmpresaData>, IEmpresaDataRepository
    {
        public EmpresaDataRepository(DatecDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
