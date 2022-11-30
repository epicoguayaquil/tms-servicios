using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsTipoCatalogo
{
    public int IdTipoCatalogo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Nemonico { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<TmsCatalogo> TmsCatalogos { get; } = new List<TmsCatalogo>();
}
