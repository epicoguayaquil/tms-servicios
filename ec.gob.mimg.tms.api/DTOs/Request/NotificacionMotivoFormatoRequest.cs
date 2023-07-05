using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class NotificacionMotivoFormatoRequest
    {
        public string Motivo { get; set; } = null!;

        public string Titulo { get; set; } = null!;

        public string Cuerpo { get; set; } = null!;

    }
}
