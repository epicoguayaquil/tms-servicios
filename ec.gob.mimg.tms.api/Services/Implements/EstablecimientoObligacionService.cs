using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class EstablecimientoObligacionService : CommonManager<TmsEstablecimientoObligacion>, IEstablecimientoObligacionService
    {
        public EstablecimientoObligacionService(TmsDbContext _dbContext) : base(new EstablecimientoObligacionRepository(_dbContext))
        {
        }

        public async Task<TmsEstablecimientoObligacion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdEstablecimientoObligacion == id);
        }
        public async Task<ICollection<TmsEstablecimientoObligacion>> GetListByEstablecimientoId(int establecimientoId)
        {
            return await GetAsync(x => x.EstablecimientoId == establecimientoId);
        }

        public async Task<TmsEstablecimientoObligacion> GetByEstablecimientoIdAndObligacionId(int establecimientoId, int obligacionId)
        {
            return await GetFirstOrDefaultAsync(x => x.EstablecimientoId == establecimientoId && x.ObligacionId == obligacionId);
        }
    }
}
