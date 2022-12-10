using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class EstablecimientoExtraDataRequest
    {
        public int IdEstablecimiento { get; set; }

        public string? NombreComercial { get; set; }

    }
}
