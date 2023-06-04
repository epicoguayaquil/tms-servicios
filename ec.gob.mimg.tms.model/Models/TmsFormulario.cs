using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsFormulario
{
    public int IdFormulario { get; set; }

    public string? Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public int EstablecimientoId { get; set; }

    public int? PasoCreacionActual { get; set; }

    public virtual TmsEstablecimiento Establecimiento { get; set; } = null!;

    public virtual ICollection<TmsFormularioActividad> TmsFormularioActividads { get; } = new List<TmsFormularioActividad>();

    public virtual ICollection<TmsFormularioDetalle> TmsFormularioDetalles { get; } = new List<TmsFormularioDetalle>();

    public virtual ICollection<TmsFormularioObligacion> TmsFormularioObligacions { get; } = new List<TmsFormularioObligacion>();
}
