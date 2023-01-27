using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class ConfiguracionObligacion
{
    public long Secuencial { get; set; }

    public long? IdActividadComercial { get; set; }

    public long? IdObligacion { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }
}
