namespace ec.gob.mimg.tms.srv.mail.Models
{
    public class NotificacionRequest
    {
        public string username { get; set; }
        public string mail { get; set; }
        public string titulo { get; set; }
        public string contenido { get; set; }
    }
}
