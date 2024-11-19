using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RentCarsham.Models;

public partial class RentCarshamContext : DbContext
{
    public RentCarshamContext()
    {
    }

    public RentCarshamContext(DbContextOptions<RentCarshamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alquilere> Alquileres { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Seguro> Seguros { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alquilere>(entity =>
        {
            entity.HasKey(e => e.AlquilerId).HasName("PK__Alquiler__F28020B57F83BCA9");

            entity.Property(e => e.FechaAlquiler).HasColumnType("datetime");
            entity.Property(e => e.FechaDevolucion).HasColumnType("datetime");
            entity.Property(e => e.TotalPago).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alquilere__Usuar__1DE57479");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alquilere__Vehic__1ED998B2");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.MantenimientoId).HasName("PK__Mantenim__A62E61A2C6DE808C");

            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaMantenimiento).HasColumnType("datetime");
            entity.Property(e => e.TipoMantenimiento)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mantenimi__Vehic__276EDEB3");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.MarcaId).HasName("PK__Marcas__D5B1CD8BFFE75C72");

            entity.HasIndex(e => e.Nombre, "UQ__Marcas__75E3EFCF6D3D6AAB").IsUnique();

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.ModeloId).HasName("PK__Modelos__FA60529A21A7EC68");

            entity.Property(e => e.Caja)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Marca).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.MarcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Modelos__MarcaId__164452B1");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__F00B6138C59EB0A8");

            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MontoPagado).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Alquiler).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.AlquilerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__AlquilerI__21B6055D");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ReservaId).HasName("PK__Reservas__C39937633042D796");

            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaReserva).HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__Usuari__2A4B4B5E");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__Vehicu__2B3F6F97");
        });

        modelBuilder.Entity<Seguro>(entity =>
        {
            entity.HasKey(e => e.SeguroId).HasName("PK__Seguros__8B87D00A2DD844AD");

            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoSeguro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.Seguros)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seguros__Vehicul__24927208");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.SucursalId).HasName("PK__Sucursal__6CB482E10B2C0321");

            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B86C910C35");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534BBA585F7").IsUnique();

            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DocumentoIdentidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.VehiculoId).HasName("PK__Vehiculo__AA088600294C68B2");

            entity.HasIndex(e => e.Placa, "UQ__Vehiculo__8310F99D214C4DC6").IsUnique();

            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PrecioPorDia).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Marca).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.MarcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculos__Marca__1A14E395");

            entity.HasOne(d => d.Modelo).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.ModeloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculos__Model__1B0907CE");

            entity.HasMany(d => d.Sucursals).WithMany(p => p.Vehiculos)
                .UsingEntity<Dictionary<string, object>>(
                    "VehiculosSucursale",
                    r => r.HasOne<Sucursale>().WithMany()
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Vehiculos__Sucur__30F848ED"),
                    l => l.HasOne<Vehiculo>().WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Vehiculos__Vehic__300424B4"),
                    j =>
                    {
                        j.HasKey("VehiculoId", "SucursalId").HasName("PK__Vehiculo__ACC3CE2ECA68AD39");
                        j.ToTable("VehiculosSucursales");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}