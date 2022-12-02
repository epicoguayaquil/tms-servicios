using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsPermiso
{
    public int IdPermiso { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<TmsRol> TmsRols { get; } = new List<TmsRol>();
}
