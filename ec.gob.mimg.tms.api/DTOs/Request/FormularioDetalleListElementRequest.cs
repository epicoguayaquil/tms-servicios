
namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioDetalleListElementRequest
    {
        public string? Caracteristica { get; set; }

        public string? TipoDato { get; set; }

        public string? Valor { get; set; }

        public string? ExtraInfo { get; set; }

        public IFormFile? ArchivoImagen { get; set; }

    }
}
