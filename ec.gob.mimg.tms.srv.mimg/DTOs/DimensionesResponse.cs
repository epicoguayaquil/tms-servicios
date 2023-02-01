using System;
using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
    public class DimensionesResponse
    {
        public ResultadoModel Resultado { get; set; }
        public List<DimensionesMinimas> DataResult { get; set; }

    }

    public class DimensionesMinimas
    {
        public string Area { get; set; }
        public string Frente { get; set; }
    }

}

