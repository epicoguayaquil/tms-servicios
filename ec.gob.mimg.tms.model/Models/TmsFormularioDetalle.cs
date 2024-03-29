﻿using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsFormularioDetalle
{
    public int IdFormularioDetalle { get; set; }

    public string? Caracteristica { get; set; }

    /// <summary>
    /// String, Integer, Float
    /// </summary>
    public string? TipoDato { get; set; }

    public string? Valor { get; set; }

    public DateTime? Fecha { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public int? FormularioId { get; set; }

    public int? PasoCreacion { get; set; }

    public string? ExtraInfo { get; set; }

    public virtual TmsFormulario? Formulario { get; set; }
}
