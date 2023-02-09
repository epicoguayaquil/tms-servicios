using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class ObligacionService : CommonManager<TmsObligacion>, IObligacionService
    {
        public ObligacionService(TmsDbContext _dbContext) : base(new ObligacionRepository(_dbContext))
        {
        }

        public async Task<TmsObligacion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdObligacion == id);
        }

        public async Task<ICollection<TmsObligacion>> GetListByJerarquiaAndEstado(string jerarquia, string estado)
        {
            return await GetAsync(x => x.Jerarquia == jerarquia && x.Estado == estado);
        }
    }
}
