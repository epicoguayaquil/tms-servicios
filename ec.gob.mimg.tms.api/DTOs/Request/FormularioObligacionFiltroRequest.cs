namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioObligacionFiltroRequest
    {
        public string? RUC { get; set; }

        public string TipoGeneracion { get; set; }

        public string Estado { get; set; }

        public int? EstablecimientoId { get; set; }

        public int? Anio { get; set; }
    }
}
