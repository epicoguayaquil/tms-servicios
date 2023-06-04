namespace ec.gob.mimg.tms.srv.mail.Models
{
    public class MailRequest
    {

        public string template { get; set; }

        public string recieverid { get; set; }

        public string  recievername { get; set; }

        public string recieveremail { get; set; }

        public string bcc { get; set; }

        public string subject { get; set; }

        public string content { get; set; }

        public string format { get; set; }

    }
}
