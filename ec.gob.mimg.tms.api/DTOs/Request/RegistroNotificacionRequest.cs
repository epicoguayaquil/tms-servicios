using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class RegistroNotificacionRequest
    {
        public DateTime FechaEnvio { get; set; }

        public string Jerarquia { get; set; } = null!;

        public int JerarquiaObjetoId { get; set; }

        public string Motivo { get; set; } = null!;

        public string Titulo { get; set; } = null!;

        public string Cuerpo { get; set; } = null!;

        public string Destinatarios { get; set; } = null!;

    }
}
