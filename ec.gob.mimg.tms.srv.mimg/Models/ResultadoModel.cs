using System;
namespace ec.gob.mimg.tms.srv.mimg.Models
{
	public class ResultadoModel
	{
        public bool Ok { get; set; }
        public string Titulo { get; set; }
        public int TipoMensaje { get; set; }
        public bool ErrorValidacion { get; set; }
        public int StatusCode { get; set; }
    }
}

