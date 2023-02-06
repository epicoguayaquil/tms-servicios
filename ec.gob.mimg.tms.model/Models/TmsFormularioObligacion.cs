using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsFormularioObligacion
{
    public int IdFormularioObligacion { get; set; }

    public string? Estado { get; set; }

    public string? Observacion { get; set; }

    public DateTime? FechaExigibilidad { get; set; }

    public DateTime? FechaRenovacion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public int ObligacionId { get; set; }

    public int FormularioId { get; set; }

    public virtual TmsFormulario Formulario { get; set; } = null!;

    public virtual TmsObligacion Obligacion { get; set; } = null!;

    public virtual ICollection<TmsFormularioObligacionCaracteristicaValor> TmsFormularioObligacionCaracteristicaValors { get; } = new List<TmsFormularioObligacionCaracteristicaValor>();
}
