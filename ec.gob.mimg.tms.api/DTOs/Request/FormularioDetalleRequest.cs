
namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioDetalleRequest
    {
        public int? FormularioId { get; set; }

        public string? Caracteristica { get; set; }

        public string? TipoDato { get; set; }

        public string? Valor { get; set; }

        public DateTime? Fecha { get; set; }

        public int? PasoCreacion { get; set; }
    }
}
