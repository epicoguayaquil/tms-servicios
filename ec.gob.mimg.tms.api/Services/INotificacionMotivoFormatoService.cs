using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface INotificacionMotivoFormatoService : ICommonManager<TmsNotificacionMotivoFormato>
    {
        Task<TmsNotificacionMotivoFormato> GetById(int id);

        Task<TmsNotificacionMotivoFormato> GetByMotivo(string motivo);

    }
}
