using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsActividadEconomica
{
    public int IdActividadEconomica { get; set; }

    public string? Nombre { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public int? Nivel { get; set; }

    public virtual ICollection<TmsActividadObligacion> TmsActividadObligacions { get; } = new List<TmsActividadObligacion>();

    public virtual ICollection<TmsFormularioActividad> TmsFormularioActividads { get; } = new List<TmsFormularioActividad>();
}
