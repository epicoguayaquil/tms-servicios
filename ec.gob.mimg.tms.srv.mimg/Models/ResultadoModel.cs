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


        public static ResultadoModel setError() {
            return new ResultadoModel()
            {
                Ok= false,
                Titulo="Error en de ejecución, favor revisar con el administrador",
                TipoMensaje=0,
                ErrorValidacion=false,
                StatusCode=500
            };
        }

    
    }


}

