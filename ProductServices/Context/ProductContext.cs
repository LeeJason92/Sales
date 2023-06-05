using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductServices.Models;

namespace ProductServices.Context
{
    public partial class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MProduct> MProducts { get; set; } = null!;
        public virtual DbSet<MProductType> MProductTypes { get; set; } = null!;
        public virtual DbSet<MSparepart> MSpareparts { get; set; } = null!;
        public virtual DbSet<MSparepartType> MSparepartTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:ProductConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MProduct>(entity =>
            {
                entity.HasKey(e => e.ProductNo);

                entity.ToTable("M_Product");

                entity.Property(e => e.ProductNo).ValueGeneratedNever();

                entity.Property(e => e.Cogs).HasColumnName("COGS");

                entity.Property(e => e.ProductBrand)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uom)
                    .HasMaxLength(50)
                    .HasColumnName("UOM")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MProductType>(entity =>
            {
                entity.HasKey(e => e.ProductTypeNo);

                entity.ToTable("M_ProductType");

                entity.Property(e => e.ProductTypeNo).ValueGeneratedNever();

                entity.Property(e => e.ProductTypeName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MSparepart>(entity =>
            {
                entity.HasKey(e => e.SparepartNo);

                entity.ToTable("M_Sparepart");

                entity.Property(e => e.SparepartNo).ValueGeneratedNever();

                entity.Property(e => e.SparepartDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SparepartType).HasDefaultValueSql("('')");

                entity.Property(e => e.Uom)
                    .HasMaxLength(50)
                    .HasColumnName("UOM")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MSparepartType>(entity =>
            {
                entity.HasKey(e => e.SparpartTypeNo);

                entity.ToTable("M_SparepartType");

                entity.Property(e => e.SparpartTypeNo).ValueGeneratedNever();

                entity.Property(e => e.SparepartTypeName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
