using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsActividadObligacion
{
    public int IdActividadObligacion { get; set; }

    public int ActividadEconomicaId { get; set; }

    public int ObligacionId { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual TmsActividadEconomica ActividadEconomica { get; set; } = null!;

    public virtual TmsObligacion Obligacion { get; set; } = null!;
}
