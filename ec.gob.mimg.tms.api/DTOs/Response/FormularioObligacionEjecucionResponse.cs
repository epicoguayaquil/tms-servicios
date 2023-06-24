namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class FormularioObligacionEjecucionResponse
    {
        public int IdFormularioObligacionEjecucion { get; set; }

        public int FormularioObligacionId { get; set; }

        public DateTime FechaEjecucion { get; set; }

        public string EstadoInicial { get; set; } = null!;

        public string EstadoFinal { get; set; } = null!;

        public int? RespuestaCode { get; set; }

        public string? RespuestaEstado { get; set; }

        public string? RespuestaDetalle { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string? Estado { get; set; }
    }
}
