using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ec.gob.mimg.tms.model.Models;

public partial class TmsDbContext : DbContext
{
    public TmsDbContext()
    {
    }

    public TmsDbContext(DbContextOptions<TmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConfiguracionObligacion> ConfiguracionObligacions { get; set; }

    public virtual DbSet<FormularioDetalle> FormularioDetalles { get; set; }

    public virtual DbSet<FormularioObligacion> FormularioObligacions { get; set; }

    public virtual DbSet<Renovacion> Renovacions { get; set; }

    public virtual DbSet<TmsActividadEconomica> TmsActividadEconomicas { get; set; }

    public virtual DbSet<TmsActividadObligacion> TmsActividadObligacions { get; set; }

    public virtual DbSet<TmsCatalogo> TmsCatalogos { get; set; }

    public virtual DbSet<TmsEmpresa> TmsEmpresas { get; set; }

    public virtual DbSet<TmsEstablecimiento> TmsEstablecimientos { get; set; }

    public virtual DbSet<TmsFormulario> TmsFormularios { get; set; }

    public virtual DbSet<TmsFormularioActividad> TmsFormularioActividads { get; set; }

    public virtual DbSet<TmsObligacion> TmsObligacions { get; set; }

    public virtual DbSet<TmsObligacionCaracteristica> TmsObligacionCaracteristicas { get; set; }

    public virtual DbSet<TmsPermiso> TmsPermisos { get; set; }

    public virtual DbSet<TmsRol> TmsRols { get; set; }

    public virtual DbSet<TmsTipoCatalogo> TmsTipoCatalogos { get; set; }

    public virtual DbSet<TmsUsuario> TmsUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=db.cloudtek.ec;Database=TMS_DB;uid=sa;pwd=DeveloperDB1.;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfiguracionObligacion>(entity =>
        {
            entity.HasKey(e => e.Secuencial).HasName("PK__Configur__32F8CA8649CE4C8F");

            entity.ToTable("ConfiguracionObligacion");

            entity.Property(e => e.Secuencial).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FormularioDetalle>(entity =>
        {
            entity.HasKey(e => e.IdFormularioDetalle).HasName("PK__Formular__9507C3AE7090239F");

            entity.ToTable("FormularioDetalle");

            entity.Property(e => e.IdFormularioDetalle).ValueGeneratedNever();
            entity.Property(e => e.Caracteristica)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Formulario).WithMany(p => p.FormularioDetalles)
                .HasForeignKey(d => d.FormularioId)
                .HasConstraintName("fk_FormularioDetalle_TmsFormulario_1");
        });

        modelBuilder.Entity<FormularioObligacion>(entity =>
        {
            entity.HasKey(e => e.IdObligacion).HasName("PK__Formular__AC85825E21BFCD71");

            entity.ToTable("FormularioObligacion");

            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Observacion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Formulario).WithMany(p => p.FormularioObligacions)
                .HasForeignKey(d => d.FormularioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FormularioObligacion_TmsFormulario_1");

            entity.HasOne(d => d.Obligacion).WithMany(p => p.FormularioObligacions)
                .HasForeignKey(d => d.ObligacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FormularioObligacion_TmsObligacion_1");
        });

        modelBuilder.Entity<Renovacion>(entity =>
        {
            entity.HasKey(e => e.IdRenovacion).HasName("PK__Renovaci__9F986BC72F00B2C0");

            entity.ToTable("Renovacion");

            entity.Property(e => e.IdRenovacion).ValueGeneratedNever();
        });

        modelBuilder.Entity<TmsActividadEconomica>(entity =>
        {
            entity.HasKey(e => e.IdActividadEconomica).HasName("PK__TmsActiv__A30407107F74EE53");

            entity.ToTable("TmsActividadEconomica");

            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2048)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsActividadObligacion>(entity =>
        {
            entity.HasKey(e => e.IdActividadObligacion).HasName("PK__TmsActiv__F9048FEF70F719F8");

            entity.ToTable("TmsActividadObligacion");

            entity.Property(e => e.IdActividadObligacion).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.ActividadEconomica).WithMany(p => p.TmsActividadObligacions)
                .HasForeignKey(d => d.ActividadEconomicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsActividadObligacion_TmsActividadEconomica_1");

            entity.HasOne(d => d.Obligacion).WithMany(p => p.TmsActividadObligacions)
                .HasForeignKey(d => d.ObligacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsActividadObligacion_TmsObligacion_1");
        });

        modelBuilder.Entity<TmsCatalogo>(entity =>
        {
            entity.HasKey(e => e.IdCatalogo).HasName("PK__TmsCatal__FD0AC26CB6748DF7");

            entity.ToTable("TmsCatalogo");

            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nemonico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.TipoCatalogo).WithMany(p => p.TmsCatalogos)
                .HasForeignKey(d => d.TipoCatalogoId)
                .HasConstraintName("fk_TmsCatalogo_TmsTipoCatalogo_1");
        });

        modelBuilder.Entity<TmsEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__TmsEmpre__5EF4033E5EF7AF15");

            entity.ToTable("TmsEmpresa");

            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsEstablecimiento>(entity =>
        {
            entity.HasKey(e => e.IdEstablecimiento).HasName("PK__TmsEstab__5C5599D472D85B85");

            entity.ToTable("TmsEstablecimiento");

            entity.Property(e => e.IdEstablecimiento).ValueGeneratedNever();
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Jurisdiccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Lat)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Lon)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SrifechaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("SRIFechaActualizacion");
            entity.Property(e => e.SrifechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("SRIFechaRegistro");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Empresa).WithMany(p => p.TmsEstablecimientos)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("fk_TmsEstablecimiento_TmsEmpresa_1");
        });

        modelBuilder.Entity<TmsFormulario>(entity =>
        {
            entity.HasKey(e => e.IdFormulario).HasName("PK__TmsFormu__090ED3C57A5C62C1");

            entity.ToTable("TmsFormulario");

            entity.Property(e => e.IdFormulario).ValueGeneratedNever();
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Establecimiento).WithMany(p => p.TmsFormularios)
                .HasForeignKey(d => d.EstablecimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsFormulario_TmsEstablecimiento_1");
        });

        modelBuilder.Entity<TmsFormularioActividad>(entity =>
        {
            entity.HasKey(e => e.IdActividadFormulario).HasName("PK__TmsFormu__D52002B9D3BA9F41");

            entity.ToTable("TmsFormularioActividad");

            entity.Property(e => e.IdActividadFormulario).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Observacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.ActividadEconomica).WithMany(p => p.TmsFormularioActividads)
                .HasForeignKey(d => d.ActividadEconomicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsFormularioActividad_TmsActividadEconomica_1");

            entity.HasOne(d => d.Formulario).WithMany(p => p.TmsFormularioActividads)
                .HasForeignKey(d => d.FormularioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsFormularioActividad_TmsFormulario_1");
        });

        modelBuilder.Entity<TmsObligacion>(entity =>
        {
            entity.HasKey(e => e.IdObligacion).HasName("PK__TmsOblig__AC85825EF7813001");

            entity.ToTable("TmsObligacion");

            entity.Property(e => e.IdObligacion).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaExigibilidad)
                .HasComment("Fecha permitida para gestionar la aprobación de la obligación")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FechaRenovacion)
                .HasComment("Aplica cuando a una fecha definida de renovación ej: 01.01.YY")
                .HasColumnType("datetime");
            entity.Property(e => e.Jerarquia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Empresa o Establecimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Origen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Municipio o SRI");
            entity.Property(e => e.TiempoVigencia).HasComment("La vigencia se maneja en meses");
            entity.Property(e => e.UsuarioModifcacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsObligacionCaracteristica>(entity =>
        {
            entity.HasKey(e => e.IdOblifacionCaracteristica).HasName("PK__TmsOblig__380CEE7385B8C90B");

            entity.ToTable("TmsObligacionCaracteristica");

            entity.Property(e => e.IdOblifacionCaracteristica).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaResgitro).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("General, Accion, Disparador");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Obligacion).WithMany(p => p.TmsObligacionCaracteristicas)
                .HasForeignKey(d => d.ObligacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsObligacionCaracteristica_TmsObligacion_1");
        });

        modelBuilder.Entity<TmsPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__TmsPermi__0D626EC82E83A22A");

            entity.ToTable("TmsPermiso");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsRol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__TmsRol__2A49584C4B9F1227");

            entity.ToTable("TmsRol");

            entity.Property(e => e.Estado)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Permiso).WithMany(p => p.TmsRols)
                .HasForeignKey(d => d.PermisoId)
                .HasConstraintName("fk_TmsRol_TmsPermiso_1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TmsRols)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("fk_TmsRol_TmsUsuario_1");
        });

        modelBuilder.Entity<TmsTipoCatalogo>(entity =>
        {
            entity.HasKey(e => e.IdTipoCatalogo).HasName("PK__TmsTipoC__40FEE08C3715FC9C");

            entity.ToTable("TmsTipoCatalogo");

            entity.Property(e => e.IdTipoCatalogo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nemonico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__TmsUsuar__5B65BF97A2904EC3");

            entity.ToTable("TmsUsuario");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Usuario)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
