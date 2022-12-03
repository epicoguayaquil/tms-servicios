using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class EmpresaService : CommonManager<TmsEmpresa>, IEmpresaService
    {
        public EmpresaService(TmsDbContext _dbContext) : base(new EmpresaRepository(_dbContext))
        {

        }

        public async Task<TmsEmpresa> GetByRucAsync(string ruc)
        {
            return await GetFirstOrDefaultAsync(x => x.Ruc == ruc);
        }

    }
}
