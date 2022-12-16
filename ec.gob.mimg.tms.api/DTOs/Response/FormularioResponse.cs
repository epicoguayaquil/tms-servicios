using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class FormularioResponse
    {
        public int IdFormulario { get; set; }

        public int EstablecimientoId { get; set; }

        public int? PasoCreacionActual { get; set; }

        public string? Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public virtual ICollection<FormularioActividadResponse> FormularioActividadResponseList { get; set; } = new List<FormularioActividadResponse>();
    }
}
