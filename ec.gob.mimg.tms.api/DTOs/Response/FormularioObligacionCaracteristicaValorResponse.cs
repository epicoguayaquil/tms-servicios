namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class FormularioObligacionCaracteristicaValorResponse
    {
        public int IdFormularioObligacionCaracteristicaValor { get; set; }

        public int ObligacionCaracteristicaId { get; set; }

        public int? FormularioObligacionId { get; set; }

        public string Nombre { get; set; } = null!;

        /// <summary>
        /// String, Integer, Float
        /// </summary>
        public string? TipoDato { get; set; }

        public string? Valor { get; set; }

        public string Estado { get; set; } = null!;

        public DateTime FechaResgitro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
