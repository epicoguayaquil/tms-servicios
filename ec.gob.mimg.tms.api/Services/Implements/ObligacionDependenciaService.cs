using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class ObligacionDependenciaService : CommonManager<TmsObligacionDependencia>, IObligacionDependenciaService
    {
        public ObligacionDependenciaService(TmsDbContext _dbContext) : base(new ObligacionDependenciaRepository(_dbContext))
        {
        }

        public async Task<TmsObligacionDependencia> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdObligacionDependencia == id);
        }

        public async Task<ICollection<TmsObligacionDependencia>> GetListaPadresByIdHijo(int hijoId)
        {
            return await GetAsync(x => x.ObligacionHijoId == hijoId);
        }

        public async Task<ICollection<TmsObligacionDependencia>> GetListaHijosByIdPadre(int padreId)
        {
            return await GetAsync(x => x.ObligacionPadreId == padreId);
        }
    }
}
