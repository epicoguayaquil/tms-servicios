using System;
using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
	public class ActividadApiResponse
	{
        public ResultadoModel Resultado { get; set; }
        public List<ActividadModel> DataResult { get; set; }

    }
}

