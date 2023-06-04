namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioObligacionCaracteristicaValorRequest
    {
        public int ObligacionCaracteristicaId { get; set; }

        public int? FormularioObligacionId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? TipoDato { get; set; }

        public string? Valor { get; set; }


    }
}
