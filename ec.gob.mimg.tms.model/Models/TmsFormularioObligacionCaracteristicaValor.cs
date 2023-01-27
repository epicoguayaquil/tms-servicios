using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsFormularioObligacionCaracteristicaValor
{
    public int IdFormularioObligacionCaracteristicaValor { get; set; }

    public int ObligacionCaracteristicaId { get; set; }

    public int? FormularioObligacionId { get; set; }

    public string Nombre { get; set; } = null!;

    /// <summary>
    /// String, Integer, Float
    /// </summary>
    public string? TipoDato { get; set; }

    public string? Valor { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaResgitro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual TmsFormularioObligacion? FormularioObligacion { get; set; }

    public virtual TmsObligacionCaracteristica ObligacionCaracteristica { get; set; } = null!;
}
