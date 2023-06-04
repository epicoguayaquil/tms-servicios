using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ec.gob.mimg.tms.srv.sri.Models;

public partial class DatecDbContext : DbContext
{
    public DatecDbContext()
    {
    }

    public DatecDbContext(DbContextOptions<DatecDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmpresaData> EmpresaData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=db.cloudtek.ec;Database=Datec_DB;uid=sa;pwd=DeveloperDB1.;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmpresaData>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ActividadEconomica).HasColumnName("ACTIVIDAD_ECONOMICA");
            entity.Property(e => e.ClaseContribuyente).HasColumnName("CLASE_CONTRIBUYENTE");
            entity.Property(e => e.CodigoCiiu).HasColumnName("CODIGO_CIIU");
            entity.Property(e => e.DescripcionCantonEst).HasColumnName("DESCRIPCION_CANTON_EST");
            entity.Property(e => e.DescripcionParroquiaEst).HasColumnName("DESCRIPCION_PARROQUIA_EST");
            entity.Property(e => e.DescripcionProvinciaEst).HasColumnName("DESCRIPCION_PROVINCIA_EST");
            entity.Property(e => e.EstadoContribuyente).HasColumnName("ESTADO_CONTRIBUYENTE");
            entity.Property(e => e.EstadoEstablecimiento).HasColumnName("ESTADO_ESTABLECIMIENTO");
            entity.Property(e => e.FechaActualizacion).HasColumnName("FECHA_ACTUALIZACION");
            entity.Property(e => e.FechaInicioActividades).HasColumnName("FECHA_INICIO_ACTIVIDADES");
            entity.Property(e => e.FechaReinicioActividades).HasColumnName("FECHA_REINICIO_ACTIVIDADES");
            entity.Property(e => e.FechaSuspensionDefinitiva).HasColumnName("FECHA_SUSPENSION_DEFINITIVA");
            entity.Property(e => e.NombreComercial).HasColumnName("NOMBRE_COMERCIAL");
            entity.Property(e => e.NombreFantasiaComercial).HasColumnName("NOMBRE_FANTASIA_COMERCIAL");
            entity.Property(e => e.NumeroEstablecimiento).HasColumnName("NUMERO_ESTABLECIMIENTO");
            entity.Property(e => e.NumeroRuc).HasColumnName("NUMERO_RUC");
            entity.Property(e => e.Obligado).HasColumnName("OBLIGADO");
            entity.Property(e => e.ProvinciaJurisdiccion).HasColumnName("PROVINCIA_JURISDICCION");
            entity.Property(e => e.RazonSocial).HasColumnName("RAZON_SOCIAL");
            entity.Property(e => e.TipoContribuyente).HasColumnName("TIPO_CONTRIBUYENTE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
