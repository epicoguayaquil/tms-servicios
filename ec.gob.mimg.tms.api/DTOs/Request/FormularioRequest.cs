using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioRequest
    {
        public int IdFormulario { get; set; }

        public int EstablecimientoId { get; set; }

        public int? PasoCreacionActual { get; set; }

    }
}
