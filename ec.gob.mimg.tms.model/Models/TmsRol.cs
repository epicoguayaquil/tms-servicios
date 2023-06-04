using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsRol
{
    public int IdRol { get; set; }

    public int? UsuarioId { get; set; }

    public int? PermisoId { get; set; }

    public string? Estado { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual TmsPermiso? Permiso { get; set; }

    public virtual TmsUsuario? Usuario { get; set; }
}
