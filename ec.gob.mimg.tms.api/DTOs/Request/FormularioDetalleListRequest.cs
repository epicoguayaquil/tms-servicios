
using ec.gob.mimg.tms.api.DTOs.Response;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioDetalleListRequest
    {
        public int FormularioId { get; set; }

        public int PasoCreacion { get; set; }

        public List<FormularioDetalleListElementRequest>? CaracteristicaList { get; set; } = new List<FormularioDetalleListElementRequest>();
    }
}
