using System;
using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
	public class DeclaracionResponse
	{
        public ResultadoModel Resultado { get; set; }
        public List<ActivoMail> DataResult { get; set; }

    }

    public class ActivoMail
    {
        public string Estado { get; set; }
        public string Anio { get; set; }
        public string FechaVencimiento { get; set; }
        public string Valor { get; set; }
    }
}

