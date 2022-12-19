using ec.gob.mimg.tms.srv.mail.Models;

namespace ec.gob.mimg.tms.srv.mail.Services
{
    public interface IMailService
    {
        Task<MailResponse> SendMail(MailRequest mailRequest);
    }
}
