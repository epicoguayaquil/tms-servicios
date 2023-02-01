using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsEstablecimientoObligacion
{
    public int IdEstablecimientoObligacion { get; set; }

    public int EstablecimientoId { get; set; }

    public int ObligacionId { get; set; }

    public string? Observacion { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual TmsEstablecimiento Establecimiento { get; set; } = null!;

    public virtual TmsObligacion Obligacion { get; set; } = null!;
}
