using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class NotificacionMotivoFormatoResponse
    {
        public int IdNotificacionMotivoFormato { get; set; }

        public string Motivo { get; set; } = null!;

        public string Titulo { get; set; } = null!;

        public string Cuerpo { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime? FechaModificaion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
