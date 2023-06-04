using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsCatalogo
{
    public int IdCatalogo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Nemonico { get; set; }

    public string? Codigo { get; set; }

    public int? Orden { get; set; }

    public string? Estado { get; set; }

    public int? TipoCatalogoId { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual TmsTipoCatalogo? TipoCatalogo { get; set; }
}
