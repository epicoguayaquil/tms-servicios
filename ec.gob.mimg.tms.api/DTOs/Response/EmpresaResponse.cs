using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.api.Data;

namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class EmpresaResponse
    {
        public int IdEmpresa { get; set; }

        public string Ruc { get; set; } = null!;

        public string? NombreComercial { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Estado { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime? FechaRegistro { get; set; }

    }
}
