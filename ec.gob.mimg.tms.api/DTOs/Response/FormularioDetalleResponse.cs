namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class FormularioDetalleResponse
    {
        public int IdFormularioDetalle { get; set; }

        public int? FormularioId { get; set; }

        public string? Caracteristica { get; set; }

        public string? TipoDato { get; set; }
        public string? Valor { get; set; }

        public DateTime? Fecha { get; set; }

        public int? PasoCreacion { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

    }
}
