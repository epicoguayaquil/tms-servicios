namespace ec.gob.mimg.tms.srv.mail.Models
{
    public class MailResponse
    {

        public List<SendResponse> mailResponseList { get; set; }
    }

    public class SendResponse
    {
        public string Type { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
    }

}
