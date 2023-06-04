namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class ObjetoObligacionResponse
    {
        public int IdObjetoObligacion { get; set; }

        public int ObligacionId { get; set; }

        public int ReferenciaId { get; set; }

        public string? Estado { get; set; }

        public string? Observacion { get; set; }

        public DateTime? FechaExigibilidad { get; set; }

        public DateTime? FechaRenovacion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public string? NombreObligacion { get; set; }

        public int? OrdenEjecucion { get; set; }

        public int Nivel { get; set; }

        public IEnumerable<ObligacionDependenciaResponse>? Dependencias { get; set; }

        public bool? SePuedeGestionar { get; set; }

        public string? TipoGeneracion { get; set; }

        public bool? TieneFormulario { get; set; }

        public string? UrlEjecucion { get; set; }

        public bool? PermiteExcepcion { get; set; }

        public string? UrlExcepcion { get; set; }

        public string? EmpresaRuc { get; set; }

        public string? EmpresaNombre { get; set; }

        public string? EstablecimientoNumero { get; set; }

        public string? EstablecimientoNombre { get; set; }
    }
}
