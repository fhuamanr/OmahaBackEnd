using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Omaha.Infra.Models;

namespace Omaha.Infra.Context
{
    public partial class OmahaContext : DbContext
    {
     
        public OmahaContext(DbContextOptions<OmahaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblFotoPerfil> TblFotoPerfils { get; set; } = null!;
        public virtual DbSet<TblGender> TblGenders { get; set; } = null!;
        public virtual DbSet<TblPdfFile> TblPdfFiles { get; set; } = null!;
        public virtual DbSet<TblPdfFilesReporte> TblPdfFilesReportes { get; set; } = null!;
        public virtual DbSet<TblPerfile> TblPerfiles { get; set; } = null!;
        public virtual DbSet<TblRole> TblRoles { get; set; } = null!;
        public virtual DbSet<TblUsuario> TblUsuarios { get; set; } = null!;
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblFotoPerfil>(entity =>
            {
                entity.ToTable("TBL_FOTO_PERFIL");

                entity.Property(e => e.FechaCarga).HasColumnType("datetime");

                entity.Property(e => e.NombreArchivo).HasMaxLength(500);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.TblFotoPerfils)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_FOTO_PERFIL_TBL_USUARIOS");
            });

            modelBuilder.Entity<TblGender>(entity =>
            {
                entity.ToTable("TBL_GENDER");

                entity.Property(e => e.Descripcion).HasMaxLength(50);
            });

            modelBuilder.Entity<TblPdfFile>(entity =>
            {
                entity.ToTable("TBL_PDF_FILES");

                entity.Property(e => e.FechaCarga).HasColumnType("datetime");

                entity.Property(e => e.NombreArchivo).HasMaxLength(500);

                entity.Property(e => e.Part).HasMaxLength(50);

                entity.Property(e => e.Periodo).HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TblPdfFiles)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_PDF_FILES_TBL_USUARIOS");
            });

            modelBuilder.Entity<TblPdfFilesReporte>(entity =>
            {
                entity.ToTable("TBL_PDF_FILES_REPORTES");

                entity.Property(e => e.FechaCarga).HasColumnType("datetime");

                entity.Property(e => e.NombreArchivo).HasMaxLength(500);

                entity.Property(e => e.Periodo).HasMaxLength(50);

                entity.Property(e => e.TpoFondo).HasMaxLength(50);
            });

            modelBuilder.Entity<TblPerfile>(entity =>
            {
                entity.ToTable("TBL_PERFILES");

                entity.Property(e => e.ClaimType).HasMaxLength(50);

                entity.Property(e => e.ClaimValue).HasMaxLength(50);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TblPerfiles)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_PERFILES_TBL_ROLES");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.ToTable("TBL_ROLES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(255)
                    .HasColumnName("NOMBRE_ROL");

                entity.Property(e => e.Vigente).HasColumnName("VIGENTE");
            });

            modelBuilder.Entity<TblUsuario>(entity =>
            {
                entity.ToTable("TBL_USUARIOS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(255)
                    .HasColumnName("APELLIDOS");

                entity.Property(e => e.Celular).HasColumnName("CELULAR");

                entity.Property(e => e.Correo)
                    .HasMaxLength(500)
                    .HasColumnName("CORREO");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Idrol).HasColumnName("IDROL");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(255)
                    .HasColumnName("NOMBRES");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.Usuario).HasMaxLength(255);

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.TblUsuarios)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_USUARIOS_TBL_GENDER");

                entity.HasOne(d => d.IdrolNavigation)
                    .WithMany(p => p.TblUsuarios)
                    .HasForeignKey(d => d.Idrol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_USUARIOS_TBL_ROLES");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
