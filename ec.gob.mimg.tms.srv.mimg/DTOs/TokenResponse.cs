﻿using System;
namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
	public class TokenResponse
    {

        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string access_token { get; set; }

	}
}

