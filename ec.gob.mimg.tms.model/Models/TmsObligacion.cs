using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsObligacion
{
    public int IdObligacion { get; set; }

    public string? Nombre { get; set; }

    /// <summary>
    /// La vigencia se maneja en meses
    /// </summary>
    public int? TiempoVigencia { get; set; }

    /// <summary>
    /// Fecha permitida para gestionar la aprobación de la obligación
    /// </summary>
    public DateTime? FechaExigibilidad { get; set; }

    /// <summary>
    /// Aplica cuando a una fecha definida de renovación ej: 01.01.YY
    /// </summary>
    public DateTime? FechaRenovacion { get; set; }

    /// <summary>
    /// Municipio o SRI
    /// </summary>
    public string? Origen { get; set; }

    /// <summary>
    /// Empresa o Establecimiento
    /// </summary>
    public string? Jerarquia { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModifcacion { get; set; }

    public virtual ICollection<TmsActividadObligacion> TmsActividadObligacions { get; } = new List<TmsActividadObligacion>();

    public virtual ICollection<TmsFormularioObligacion> TmsFormularioObligacions { get; } = new List<TmsFormularioObligacion>();

    public virtual ICollection<TmsObligacionCaracteristica> TmsObligacionCaracteristicas { get; } = new List<TmsObligacionCaracteristica>();
}
