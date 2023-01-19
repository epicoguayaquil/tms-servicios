using System;
using System.Collections.Generic;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsEstablecimiento
{
    public int IdEstablecimiento { get; set; }

    public string? NombreComercial { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Lat { get; set; }

    public string? Lon { get; set; }

    /// <summary>
    /// Inhabilitado, Habilitado
    /// </summary>
    public string? Estado { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? EmpresaId { get; set; }

    public string? Jurisdiccion { get; set; }

    public DateTime? SrifechaRegistro { get; set; }

    public DateTime? SrifechaActualizacion { get; set; }

    public string? NumeroEstablecimiento { get; set; }

    /// <summary>
    /// No_Registrado, En_Proceso, Registrado
    /// </summary>
    public string EstadoRegistro { get; set; } = null!;
    
    public virtual TmsEmpresa? Empresa { get; set; }

    public virtual ICollection<TmsFormulario> TmsFormularios { get; } = new List<TmsFormulario>();
}
