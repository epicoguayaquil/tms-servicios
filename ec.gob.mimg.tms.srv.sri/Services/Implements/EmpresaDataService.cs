
using ec.gob.mimg.tms.api.Repositories;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.srv.sri.Models;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class EmpresaDataService : CommonManager<EmpresaData>, IEmpresaDataService
    {
        public EmpresaDataService(DatecDbContext _dbContext) : base(new EmpresaDataRepository(_dbContext))
        {

        }

        public async Task<EmpresaData> GetByRucAsync(string ruc)
        {
            return await GetFirstOrDefaultAsync(x => x.NumeroRuc == ruc);
        }

    }
}
