using System;
using System.Collections.Generic;
using CineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CineApi.Contexts;

public partial class CineContext : DbContext
{
    public CineContext()
    {
    }

    public CineContext(DbContextOptions<CineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<pelicula> peliculas { get; set; }

    public virtual DbSet<pelicula_sala_cine> pelicula_sala_cines { get; set; }

    public virtual DbSet<sala_cine> sala_cines { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<pelicula>(entity =>
        {
            entity.HasKey(e => e.id_pelicula).HasName("PK__pelicula__B5017F4D37A3E3FF");

            entity.ToTable("pelicula");

            entity.Property(e => e.estado).HasDefaultValue(true);
            entity.Property(e => e.nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<pelicula_sala_cine>(entity =>
        {
            entity.HasKey(e => e.id_pelicula_sala).HasName("PK__pelicula__39BC477FAF6BC83A");

            entity.ToTable("pelicula_sala_cine");

            entity.Property(e => e.estado).HasDefaultValue(true);

            entity.HasOne(d => d.id_peliculaNavigation).WithMany(p => p.pelicula_sala_cines)
                .HasForeignKey(d => d.id_pelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeliculaSala_Pelicula");

            entity.HasOne(d => d.id_salaNavigation).WithMany(p => p.pelicula_sala_cines)
                .HasForeignKey(d => d.id_sala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeliculaSala_Sala");
        });

        modelBuilder.Entity<sala_cine>(entity =>
        {
            entity.HasKey(e => e.id_sala).HasName("PK__sala_cin__D18B015B96B00908");

            entity.ToTable("sala_cine");

            entity.Property(e => e.estado).HasDefaultValue(true);
            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
