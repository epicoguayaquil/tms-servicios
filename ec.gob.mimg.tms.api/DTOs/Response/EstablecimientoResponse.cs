using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.api.Data;

namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class EstablecimientoResponse
    {
        public int IdEstablecimiento { get; set; }
   
        public int? EmpresaId { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Lat { get; set; }

        public string? Lon { get; set; }

        public string? Estado { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime? FechaRegistro { get; set; }

    }
}
