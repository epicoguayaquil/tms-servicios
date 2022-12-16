namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class FormularioActividadResponse
    {
        public int IdActividadFormulario { get; set; }

        public int ActividadEconomicaId { get; set; }

        public int FormularioId { get; set; }

        public string? Observacion { get; set; }

        public string Estado { get; set; } = null!;

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public string? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public ActividadEconomicaResponse? ActividadEconomica { get; set; }

    }
}
