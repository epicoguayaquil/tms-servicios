using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsFormulario
{
    public int IdFormulario { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public int EstablecimientoId { get; set; }

    public virtual TmsEstablecimiento Establecimiento { get; set; } = null!;

    public virtual ICollection<FormularioDetalle> FormularioDetalles { get; } = new List<FormularioDetalle>();

    public virtual ICollection<FormularioObligacion> FormularioObligacions { get; } = new List<FormularioObligacion>();

    public virtual ICollection<TmsFormularioActividad> TmsFormularioActividads { get; } = new List<TmsFormularioActividad>();
}
