namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class ObligacionActividadResponse
    {
        public int IdActividadObligacion { get; set; }

        public int ActividadEconomicaId { get; set; }

        public int ObligacionId { get; set; }

        public string? Estado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

    }
}
