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
                var connection = "Data Source=" + Path.Combine(Environment.CurrentDirectory, "dbCaseItau.s3db");
                optionsBuilder.UseSqlite(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fund>(entity =>
            {
                entity.ToTable("FUNDO");

                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasColumnName("CODIGO")
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .HasColumnName("NOME")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TaxId)
                    .HasColumnName("CNPJ")
                    .IsRequired()
                    .HasMaxLength(14);

                entity.Property(e => e.TypeCode)
                    .HasColumnName("CODIGO_TIPO")
                    .IsRequired();

                entity.Property(e => e.Equity)
                    .HasColumnName("PATRIMONIO")
                    .HasColumnType("numeric");

                entity.HasOne(d => d.Type)
                    .WithMany()
                    .HasForeignKey(d => d.TypeCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Entities.Type>(entity =>
            {
                entity.ToTable("TIPO_FUNDO");

                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasColumnName("CODIGO")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("NOME")
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
