using System;
using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
	public class PredioInfoApiResponse
    {
        public ResultadoModel Resultado { get; set; }
        public List<PredioInfoModel> DataResult { get; set; }

    }
}

