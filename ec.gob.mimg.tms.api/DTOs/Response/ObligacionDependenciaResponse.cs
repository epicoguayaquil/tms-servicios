using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class ObligacionDependenciaResponse
    {
        public int IdObligacionDependencia { get; set; }

        public int ObligacionPadreId { get; set; }

        public int ObligacionHijoId { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; } = null!;
    }
}
