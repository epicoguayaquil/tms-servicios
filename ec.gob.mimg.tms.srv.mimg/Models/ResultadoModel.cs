using System;
namespace ec.gob.mimg.tms.srv.mimg.Models
{
	public class ResultadoModel
	{
        public bool ok { get; set; }
        public string titulo { get; set; }
        public int tipoMensaje { get; set; }
        public bool errorValidacion { get; set; }
        public int statusCode { get; set; }
    }
}

