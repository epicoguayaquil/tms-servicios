using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsUsuario
{
    public int IdUsuario { get; set; }

    public string Usuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<TmsRol> TmsRols { get; } = new List<TmsRol>();
}
