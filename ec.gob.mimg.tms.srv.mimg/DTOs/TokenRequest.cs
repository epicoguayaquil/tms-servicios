using System;
namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
	public class TokenRequest
	{
        public string client_id { get; set; }
        public string scope { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }

	}
}

