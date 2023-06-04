using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
    public class EstablecimientoApiResponse
    {
        public ResultadoModel Resultado { get; set; }
        public List<EstablecimientoModel> DataResult { get; set; }
    }
}
