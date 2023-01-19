using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class ObligacionActividadService : CommonManager<TmsActividadObligacion>, IObligacionActividadService
    {
        public ObligacionActividadService(TmsDbContext _dbContext) : base(new ObligacionActividadRepository(_dbContext))
        {
        }

        public async Task<TmsActividadObligacion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdActividadObligacion == id);
        }

        public async Task<ICollection<TmsActividadObligacion>> GetListByObligacionId(int obligacionId)
        {
            return await GetAsync(x => x.ObligacionId == obligacionId);
        }

        public async Task<ICollection<TmsActividadObligacion>> GetListByActividadId(int actividadId)
        {
            return await GetAsync(x => x.ActividadEconomicaId == actividadId);
        }

    }
}
