using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class EmpresaRequest
    {
       
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(13)]
        public string Ruc { get; set; } = null!;

        public string? NombreComercial { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

    }
}
