
namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class EmpresaObligacionRequest
    {
        public int EmpresaId { get; set; }

        public int ObligacionId { get; set; }

        public string? Observacion { get; set; }

        public DateTime? FechaExigibilidad { get; set; }

        public DateTime? FechaRenovacion { get; set; }

    }
}
