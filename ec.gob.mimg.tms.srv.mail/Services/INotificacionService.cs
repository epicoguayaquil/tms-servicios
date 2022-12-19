using ec.gob.mimg.tms.srv.mail.Models;

namespace ec.gob.mimg.tms.srv.mail.Services
{
    public interface INotificacionService
    {
        Task<NotificacionResponse> EnviarNotificacion(NotificacionRequest notificacionRequest);
    }
}
