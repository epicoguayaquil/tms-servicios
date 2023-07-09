using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsNotificacion
{
    public int IdNotificacion { get; set; }

    public DateTime FechaEnvio { get; set; }

    /// <summary>
    /// Empresa, Establecimiento, Formulario, Obligacion
    /// </summary>
    public string Jerarquia { get; set; } = null!;

    public int JerarquiaObjetoId { get; set; }

    /// <summary>
    /// Nuevo establecimiento, Caducidad Obligacion, Exigibilidad de la Obligacion, Estado Tramite
    /// </summary>
    public string Motivo { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public string Cuerpo { get; set; } = null!;

    /// <summary>
    /// correos
    /// </summary>
    public string Destinatarios { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    /// <summary>
    /// SI o NO
    /// </summary>
    public string? Leido { get; set; }
}
