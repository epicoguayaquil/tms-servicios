using System;
using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
    public class CatalogoActividadResponse
    {
        public ResultadoModel Resultado { get; set; }
        public List<CatalogoActividadModel> DataResult { get; set; }
    }
}

