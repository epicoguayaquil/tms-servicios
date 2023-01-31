using System;
using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
	public class PredioApiResponse
    {
        public bool Success { get; set; }

        public bool SuccessCoordenadas { get; set; }

        public PredioModel DataResult { get; set; }

    }
}

