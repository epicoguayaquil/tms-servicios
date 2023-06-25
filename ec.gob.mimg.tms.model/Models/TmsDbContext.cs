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

    public virtual DbSet<Renovacion> Renovacions { get; set; }

    public virtual DbSet<TmsActividadEconomica> TmsActividadEconomicas { get; set; }

    public virtual DbSet<TmsActividadObligacion> TmsActividadObligacions { get; set; }

    public virtual DbSet<TmsCatalogo> TmsCatalogos { get; set; }

    public virtual DbSet<TmsEmpresa> TmsEmpresas { get; set; }

    public virtual DbSet<TmsEmpresaObligacion> TmsEmpresaObligacions { get; set; }

    public virtual DbSet<TmsEstablecimiento> TmsEstablecimientos { get; set; }

    public virtual DbSet<TmsFormulario> TmsFormularios { get; set; }

    public virtual DbSet<TmsFormularioActividad> TmsFormularioActividads { get; set; }

    public virtual DbSet<TmsFormularioDetalle> TmsFormularioDetalles { get; set; }

    public virtual DbSet<TmsFormularioObligacion> TmsFormularioObligacions { get; set; }

    public virtual DbSet<TmsFormularioObligacionCaracteristicaValor> TmsFormularioObligacionCaracteristicaValors { get; set; }

    public virtual DbSet<TmsFormularioObligacionEjecucion> TmsFormularioObligacionEjecucions { get; set; }

    public virtual DbSet<TmsNotificacion> TmsNotificacions { get; set; }

    public virtual DbSet<TmsNotificacionMotivoFormato> TmsNotificacionMotivoFormatos { get; set; }

    public virtual DbSet<TmsObligacion> TmsObligacions { get; set; }

    public virtual DbSet<TmsObligacionCaracteristica> TmsObligacionCaracteristicas { get; set; }

    public virtual DbSet<TmsObligacionDependencia> TmsObligacionDependencia { get; set; }

    public virtual DbSet<TmsPermiso> TmsPermisos { get; set; }

    public virtual DbSet<TmsRol> TmsRols { get; set; }

    public virtual DbSet<TmsTipoCatalogo> TmsTipoCatalogos { get; set; }

    public virtual DbSet<TmsUsuario> TmsUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL5105.site4now.net;Database=db_a90b97_testdbtms;uid=db_a90b97_testdbtms_admin;pwd=Datos.2023;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfiguracionObligacion>(entity =>
        {
            entity.HasKey(e => e.Secuencial).HasName("PK__Configur__32F8CA86A6E51A3B");

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

        modelBuilder.Entity<Renovacion>(entity =>
        {
            entity.HasKey(e => e.IdRenovacion).HasName("_copy_14");

            entity.ToTable("Renovacion");

            entity.Property(e => e.IdRenovacion).ValueGeneratedNever();
        });

        modelBuilder.Entity<TmsActividadEconomica>(entity =>
        {
            entity.HasKey(e => e.IdActividadEconomica).HasName("_copy_12");

            entity.ToTable("TmsActividadEconomica");

            entity.Property(e => e.Bombero)
                .HasMaxLength(255)
                .IsUnicode(false);
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
            entity.Property(e => e.Grupo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NivelImpacto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsActividadObligacion>(entity =>
        {
            entity.HasKey(e => e.IdActividadObligacion).HasName("_copy_13");

            entity.ToTable("TmsActividadObligacion");

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
            entity.HasKey(e => e.IdCatalogo).HasName("_copy_7");

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
            entity.HasKey(e => e.IdEmpresa).HasName("_copy_9");

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

        modelBuilder.Entity<TmsEmpresaObligacion>(entity =>
        {
            entity.HasKey(e => e.IdEmpresaObligacion).HasName("PK__TmsEmpre__28B879F8252FBC96");

            entity.ToTable("TmsEmpresaObligacion");

            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FechaExigibilidad).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FechaRenovacion).HasColumnType("datetime");
            entity.Property(e => e.Observacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Empresa).WithMany(p => p.TmsEmpresaObligacions)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TmsEmpresaObligacion_TmsEmpresa");

            entity.HasOne(d => d.Obligacion).WithMany(p => p.TmsEmpresaObligacions)
                .HasForeignKey(d => d.ObligacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TmsEmpresaObligacion_TmsObligacion");
        });

        modelBuilder.Entity<TmsEstablecimiento>(entity =>
        {
            entity.HasKey(e => e.IdEstablecimiento).HasName("_copy_2");

            entity.ToTable("TmsEstablecimiento");

            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Habilitado, Inhabilitado");
            entity.Property(e => e.EstadoRegistro)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("No_Registrado, En_Proceso, Registrado");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Industria)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.NumeroEstablecimiento)
                .HasMaxLength(10)
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsEstablecimiento_TmsEmpresa_1");
        });

        modelBuilder.Entity<TmsFormulario>(entity =>
        {
            entity.HasKey(e => e.IdFormulario).HasName("_copy_10");

            entity.ToTable("TmsFormulario");

            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
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
            entity.HasKey(e => e.IdActividadFormulario).HasName("_copy_11");

            entity.ToTable("TmsFormularioActividad");

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

        modelBuilder.Entity<TmsFormularioDetalle>(entity =>
        {
            entity.HasKey(e => e.IdFormularioDetalle).HasName("_copy_3");

            entity.ToTable("TmsFormularioDetalle");

            entity.Property(e => e.Caracteristica)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ExtraInfo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.TipoDato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("String, Integer, Float");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Formulario).WithMany(p => p.TmsFormularioDetalles)
                .HasForeignKey(d => d.FormularioId)
                .HasConstraintName("fk_FormularioDetalle_TmsFormulario_1");
        });

        modelBuilder.Entity<TmsFormularioObligacion>(entity =>
        {
            entity.HasKey(e => e.IdFormularioObligacion).HasName("_copy_6");

            entity.ToTable("TmsFormularioObligacion");

            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaExigibilidad).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FechaRenovacion).HasColumnType("datetime");
            entity.Property(e => e.Observacion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Formulario).WithMany(p => p.TmsFormularioObligacions)
                .HasForeignKey(d => d.FormularioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FormularioObligacion_TmsFormulario_1");

            entity.HasOne(d => d.Obligacion).WithMany(p => p.TmsFormularioObligacions)
                .HasForeignKey(d => d.ObligacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FormularioObligacion_TmsObligacion_1");
        });

        modelBuilder.Entity<TmsFormularioObligacionCaracteristicaValor>(entity =>
        {
            entity.HasKey(e => e.IdFormularioObligacionCaracteristicaValor).HasName("_copy_1");

            entity.ToTable("TmsFormularioObligacionCaracteristicaValor");

            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaResgitro).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TipoDato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("String, Integer, Float");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.FormularioObligacion).WithMany(p => p.TmsFormularioObligacionCaracteristicaValors)
                .HasForeignKey(d => d.FormularioObligacionId)
                .HasConstraintName("fk_TmsFormularioObligacionCaracteristicaValor_TmsFormularioObligacion");

            entity.HasOne(d => d.ObligacionCaracteristica).WithMany(p => p.TmsFormularioObligacionCaracteristicaValors)
                .HasForeignKey(d => d.ObligacionCaracteristicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TmsFormularioObligacionCaracteristicaValor_TmsObligacionCaracteristica");
        });

        modelBuilder.Entity<TmsFormularioObligacionEjecucion>(entity =>
        {
            entity.HasKey(e => e.IdFormularioObligacionEjecucion).HasName("PK__TmsFormu__B923ED7B9C8C0C77");

            entity.ToTable("TmsFormularioObligacionEjecucion");

            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.EstadoFinal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoInicial)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaEjecucion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.RespuestaDetalle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RespuestaEstado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FormularioObligacion).WithMany(p => p.TmsFormularioObligacionEjecucions)
                .HasForeignKey(d => d.FormularioObligacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FormularioObligacionEjecucion_FormularioObligacion");
        });

        modelBuilder.Entity<TmsNotificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK__TmsNotif__F6CA0A859D4EE9E4");

            entity.ToTable("TmsNotificacion");

            entity.Property(e => e.Cuerpo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Destinatarios)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("correos");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Jerarquia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Empresa, Establecimiento, Formulario, Obligacion");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Nuevo establecimiento, Caducidad Obligacion, Exigibilidad de la Obligacion, Estado Tramite");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsNotificacionMotivoFormato>(entity =>
        {
            entity.HasKey(e => e.IdNotificacionMotivoFormato).HasName("PK__TmsNotif__90AF373FDC8F633E");

            entity.ToTable("TmsNotificacionMotivoFormato");

            entity.Property(e => e.Cuerpo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificaion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsObligacion>(entity =>
        {
            entity.HasKey(e => e.IdObligacion).HasName("_copy_4");

            entity.ToTable("TmsObligacion");

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
            entity.Property(e => e.TipoExigibilidad)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("VENCIMIENTO, POR_MES");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsObligacionCaracteristica>(entity =>
        {
            entity.HasKey(e => e.IdObligacionCaracteristica).HasName("_copy_5");

            entity.ToTable("TmsObligacionCaracteristica");

            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaResgitro).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubTipo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("General, Accion, Disparador, Inicial");
            entity.Property(e => e.TipoDato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("String, Integer, Float");
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

        modelBuilder.Entity<TmsObligacionDependencia>(entity =>
        {
            entity.HasKey(e => e.IdObligacionDependencia).HasName("PK__TmsOblig__402B8466DE489CBD");

            entity.Property(e => e.IdObligacionDependencia).HasColumnName("idObligacionDependencia");
            entity.Property(e => e.FechaRegistro).HasColumnType("date");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmsPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("_copy_15");

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
            entity.HasKey(e => e.IdRol).HasName("_copy_17");

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
            entity.HasKey(e => e.IdTipoCatalogo).HasName("_copy_8");

            entity.ToTable("TmsTipoCatalogo");

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
            entity.HasKey(e => e.IdUsuario).HasName("_copy_16");

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
