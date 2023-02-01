using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class EstablecimientoRequest
    {
        public int? EmpresaId { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Lat { get; set; }

        public string? Lon { get; set; }

        public string? NombreComercial { get; set; }

        public string? Industria { get; set; }

        public string? Jurisdiccion { get; set; }

        public DateTime? SrifechaRegistro { get; set; }

        public DateTime? SrifechaActualizacion { get; set; }

        public string? NumeroEstablecimiento { get; set; }

    }
}
