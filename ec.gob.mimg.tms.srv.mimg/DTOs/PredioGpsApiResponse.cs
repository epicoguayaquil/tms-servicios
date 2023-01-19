using System;
using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
	public class PredioGpsApiResponse
    {
        public ResultadoModel Resultado { get; set; }
        public List<PredioGpsModel> DataResult { get; set; }

    }
}

