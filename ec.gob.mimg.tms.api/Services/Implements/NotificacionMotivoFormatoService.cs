using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class NotificacionMotivoFormatoService : CommonManager<TmsNotificacionMotivoFormato>, INotificacionMotivoFormatoService
    {
        public NotificacionMotivoFormatoService(TmsDbContext _dbContext) : base(new NotificacionMotivoFormatoRepository(_dbContext))
        {
        }

        public async Task<TmsNotificacionMotivoFormato> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdNotificacionMotivoFormato == id);
        }

        public async Task<TmsNotificacionMotivoFormato> GetByMotivo(string motivo)
        {
            return await GetFirstOrDefaultAsync(x => x.Motivo == motivo);
        }

    }
}
