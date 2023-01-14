namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class ObligacionResponse
    {
        public int IdObligacion { get; set; }

        public string? Nombre { get; set; }

        public int? TiempoVigencia { get; set; }

        public DateTime? FechaExigibilidad { get; set; }

        public DateTime? FechaRenovacion { get; set; }

        public string? Origen { get; set; }

        public string? Jerarquia { get; set; }

        public int? OrdenEjecucion { get; set; }

        public int? Dependencia { get; set; }

        public string? Estado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

    }
}
