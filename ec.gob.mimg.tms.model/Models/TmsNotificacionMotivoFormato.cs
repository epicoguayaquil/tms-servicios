using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsNotificacionMotivoFormato
{
    public int IdNotificacionMotivoFormato { get; set; }

    public string Motivo { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public string Cuerpo { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime? FechaModificaion { get; set; }

    public string? UsuarioModificacion { get; set; }
}
