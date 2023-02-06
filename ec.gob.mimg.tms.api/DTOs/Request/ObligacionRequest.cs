namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class ObligacionRequest
    {
        public string? Nombre { get; set; }

        public int? TiempoVigencia { get; set; }

        public DateTime? FechaExigibilidad { get; set; }

        public DateTime? FechaRenovacion { get; set; }

        public int? MesExigibilidad { get; set; }

        public int? MesRenovacion { get; set; }

        public string? Origen { get; set; }

        public string? Jerarquia { get; set; }

        public int? OrdenEjecucion { get; set; }

        public int? Dependencia { get; set; }

    }
}
