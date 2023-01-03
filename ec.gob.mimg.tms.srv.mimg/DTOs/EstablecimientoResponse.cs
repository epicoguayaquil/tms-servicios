using ec.gob.mimg.tms.srv.mimg.Models;

namespace ec.gob.mimg.tms.srv.mimg.DTOs
{
    public class EstablecimientoResponse
    {
        public ContribuyenteModel dataResult;
        public List<EstablecimientoModel> resultado { get; set; }
    }
}
