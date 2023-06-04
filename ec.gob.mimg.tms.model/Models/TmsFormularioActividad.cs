using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsFormularioActividad
{
    public int IdActividadFormulario { get; set; }

    public string? Observacion { get; set; }

    public string Estado { get; set; } = null!;

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int ActividadEconomicaId { get; set; }

    public int FormularioId { get; set; }

    public virtual TmsActividadEconomica ActividadEconomica { get; set; } = null!;

    public virtual TmsFormulario Formulario { get; set; } = null!;
}
