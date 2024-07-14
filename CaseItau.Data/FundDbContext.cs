using CaseItau.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace CaseItau.Data
{
    public class FundDbContext : DbContext
    {
        public DbSet<Fund> Funds { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connection = "Data Source=" + Path.Combine(Environment.CurrentDirectory, "Banco", "ConsorcieiDocs.db");
                optionsBuilder.UseSqlite(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Fund>(entity =>
        {
            entity.ToTable("FUNDO ");

            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasColumnName("CODIGO")
                .HasMaxLength(20)
                .IsRequired();

            entity.HasIndex(e => e.Code)
                .IsUnique();

            entity.Property(e => e.Name)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.TaxId)
                .HasColumnName("CNPJ")
                .HasMaxLength(14)
                .IsRequired();

            entity.HasIndex(e => e.TaxId)
                .IsUnique();

            entity.Property(e => e.TypeCode)
                .HasColumnName("CODIGO_TIPO")
                .IsRequired();

            entity.Property(e => e.Equity)
                .HasColumnName("PATRIMONIO")
                .HasColumnType("NUMERIC");

            entity.HasOne<Entities.Type>()
                .WithMany()
                .HasForeignKey(e => e.Code)
                .HasConstraintName("FK_Fund_TipoFundo");
        });

            modelBuilder.Entity<Entities.Type>(entity =>
            {
                entity.ToTable("Tipo_Fundo");

                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasColumnName("CODIGO")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("NOME")
                    .HasMaxLength(20)
                    .IsRequired();
            });            
        }
    }
}
