namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class EstablecimientoObligacionResponse
    {
        public int IdEstablecimientoObligacion { get; set; }

        public int EstablecimientoId { get; set; }

        public int ObligacionId { get; set; }

        public string? Observacion { get; set; }

        public string Estado { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public string? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

    }
}
