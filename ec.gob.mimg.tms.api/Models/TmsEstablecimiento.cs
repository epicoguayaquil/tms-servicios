﻿using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.api.Models;

public partial class TmsEstablecimiento
{
    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Lat { get; set; }

    public string? Lon { get; set; }

    public string? Estado { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? EmpresaId { get; set; }

    public int IdEstablecimiento { get; set; }

    public virtual TmsEmpresa? Empresa { get; set; }

    public virtual ICollection<TmsFormulario> TmsFormularios { get; } = new List<TmsFormulario>();
}
