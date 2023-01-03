using ec.gob.mimg.tms.srv.mail.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace ec.gob.mimg.tms.srv.mail.Services.Implements
{

    public class NotifacionService : INotificacionService
    {
        private readonly ILogger<NotifacionService> _logger;
        private readonly IMailService _mailService;

        public NotifacionService(ILogger<NotifacionService> logger, IMailService mailService)
        {
            _mailService = mailService;
            _logger = logger;
        }

        async Task<NotificacionResponse> INotificacionService.EnviarNotificacion(NotificacionRequest notificacionRequest)
        {
            _logger.LogError(">>> Generando Notifacion......{RunTime}", DateTime.Now);
            NotificacionResponse notifacionResponse = new NotificacionResponse();

            MailRequest mailRequest = new MailRequest();
           
            mailRequest.recievername = notificacionRequest.username;
            mailRequest.recieveremail = notificacionRequest.mail;
            mailRequest.subject = notificacionRequest.titulo;
            mailRequest.content = notificacionRequest.contenido;

            mailRequest.format = "2";
            mailRequest.template = "1";
            mailRequest.recieverid = "0000000000";
            mailRequest.bcc = "juanklafuente@gmail.com";

            MailResponse mailResponse = await _mailService.SendMail(mailRequest);

            if (mailResponse.mailResponseList.Count > 0)
            {
                notifacionResponse.cod = "200";
                notifacionResponse.msg = "OK";
            }
            else
            {
                notifacionResponse.cod = "500";
                notifacionResponse.msg = "Error";
            }

            return notifacionResponse;
        }


    }
}
