namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioObligacionEjecucionRequest
    {
        public int FormularioObligacionId { get; set; }

        public string EstadoInicial { get; set; } = null!;

        public string EstadoFinal { get; set; } = null!;

        public int? RespuestaCode { get; set; }

        public string? RespuestaEstado { get; set; }

        public string? RespuestaDetalle { get; set; }


    }
}
