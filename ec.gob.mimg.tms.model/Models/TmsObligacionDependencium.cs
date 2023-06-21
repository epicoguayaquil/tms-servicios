using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsObligacionDependencium
{
    public int IdObligacionDependencia { get; set; }

    public int ObligacionPadreId { get; set; }

    public int ObligacionHijoId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string UsuarioRegistro { get; set; } = null!;
}
