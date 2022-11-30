using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class FormularioObligacion
{
    public int IdObligacion { get; set; }

    public string? Estado { get; set; }

    public string? Observacion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public int ObligacionId { get; set; }

    public int FormularioId { get; set; }

    public virtual TmsFormulario Formulario { get; set; } = null!;

    public virtual TmsObligacion Obligacion { get; set; } = null!;
}
