using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class ObligacionDependenciaRequest
    {
        public int ObligacionPadreId { get; set; }

        public int ObligacionHijoId { get; set; }

    }
}
