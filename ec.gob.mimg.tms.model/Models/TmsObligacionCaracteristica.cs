using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsObligacionCaracteristica
{
    public int IdObligacionCaracteristica { get; set; }

    public int ObligacionId { get; set; }

    /// <summary>
    /// General, Accion, Disparador, Inicial
    /// </summary>
    public string? Tipo { get; set; }

    public string? SubTipo { get; set; }

    public string Nombre { get; set; } = null!;

    /// <summary>
    /// String, Integer, Float
    /// </summary>
    public string? TipoDato { get; set; }

    public string? Valor { get; set; }

    public int? Secuencia { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaResgitro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual TmsObligacion Obligacion { get; set; } = null!;

    public virtual ICollection<TmsFormularioObligacionCaracteristicaValor> TmsFormularioObligacionCaracteristicaValors { get; } = new List<TmsFormularioObligacionCaracteristicaValor>();
}
