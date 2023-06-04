using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class RegistroNotificacionResponse
    {
        public int IdNotificacion { get; set; }

        public DateTime FechaEnvio { get; set; }

        public string Jerarquia { get; set; } = null!;

        public int JerarquiaObjetoId { get; set; }

        public string Motivo { get; set; } = null!;

        public string Titulo { get; set; } = null!;

        public string Cuerpo { get; set; } = null!;

        public string Destinatarios { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
