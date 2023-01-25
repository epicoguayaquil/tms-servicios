namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class ObligacionCaracteristicaRequest
    {
        public int ObligacionId { get; set; }

        public string? Tipo { get; set; }

        public string Nombre { get; set; } = null!;

        public string? TipoDato { get; set; }

        public string? Valor { get; set; }

        public int Secuencia { get; set; }
    }
}
