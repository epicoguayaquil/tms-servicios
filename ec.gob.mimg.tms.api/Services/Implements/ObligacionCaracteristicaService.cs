using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.Enums;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class ObligacionCaracteristicaService : CommonManager<TmsObligacionCaracteristica>, IObligacionCaracteristicaService
    {
        public ObligacionCaracteristicaService(TmsDbContext _dbContext) : base(new ObligacionCaracteristicaRepository(_dbContext))
        {
        }

        public async Task<TmsObligacionCaracteristica> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdObligacionCaracteristica == id);
        }

        public async Task<ICollection<TmsObligacionCaracteristica>> GetListByObligacionId(int obligacionId)
        {
            return await GetAsync(x => x.ObligacionId == obligacionId);
        }

        public async Task<ICollection<TmsObligacionCaracteristica>> GetListByObligacionIdAndTipo(int obligacionId, string tipo)
        {
            return await GetAsync(x => x.ObligacionId == obligacionId && x.Tipo == tipo && x.Estado == EstadoEnum.ACTIVO.ToString());
        }

        public async Task<ICollection<TmsObligacionCaracteristica>> GetListByObligacionIdAndTipoANdSubTipo(int obligacionId, string tipo, string subTipo)
        {
            return await GetAsync(x => x.ObligacionId == obligacionId && x.Tipo == tipo && x.SubTipo == subTipo);
        }
    }
}
