using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsEmpresa
{
    public int IdEmpresa { get; set; }

    public string Ruc { get; set; } = null!;

    public string? NombreComercial { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Estado { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<TmsEstablecimiento> TmsEstablecimientos { get; } = new List<TmsEstablecimiento>();
}
