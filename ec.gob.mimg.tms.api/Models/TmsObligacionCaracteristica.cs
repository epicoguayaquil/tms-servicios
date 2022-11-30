using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsObligacionCaracteristica
{
    public int IdOblifacionCaracteristica { get; set; }

    public int ObligacionId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaResgitro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    /// <summary>
    /// General, Accion, Disparador
    /// </summary>
    public string? Tipo { get; set; }

    public string? Valor { get; set; }

    public virtual TmsObligacion Obligacion { get; set; } = null!;
}
