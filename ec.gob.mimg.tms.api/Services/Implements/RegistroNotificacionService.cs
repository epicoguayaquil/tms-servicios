using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class RegistroNotificacionService : CommonManager<TmsNotificacion>, IRegistroNotificacionService
    {
        public RegistroNotificacionService(TmsDbContext _dbContext) : base(new RegistroNotificacionRepository(_dbContext))
        {
        }

        public async Task<TmsNotificacion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdNotificacion == id);
        }

        public async Task<ICollection<TmsNotificacion>> GetListByJerarquiaAndObjetoId(string jerarquia, int objetoId)
        {
            return await GetAsync(x => x.Jerarquia == jerarquia && x.JerarquiaObjetoId == objetoId);
        }
    }
}
