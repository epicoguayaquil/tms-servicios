using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class EmpresaObligacionService : CommonManager<TmsEmpresaObligacion>, IEmpresaObligacionService
    {
        public EmpresaObligacionService(TmsDbContext _dbContext) : base(new EstablecimientoObligacionRepository(_dbContext))
        {
        }

        public async Task<TmsEmpresaObligacion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdEmpresaObligacion == id);
        }
        public async Task<ICollection<TmsEmpresaObligacion>> GetListByEmpresaId(int empresaId)
        {
            return await GetAsync(x => x.EmpresaId == empresaId);
        }

        public async Task<TmsEmpresaObligacion> GetByEmpresaIdAndObligacionId(int empresaId, int obligacionId)
        {
            return await GetFirstOrDefaultAsync(x => x.EmpresaId == empresaId && x.ObligacionId == obligacionId);
        }
    }
}
