using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WarehouseServices.Models;

namespace WarehouseServices.Context
{
    public partial class WarehouseContext : DbContext
    {
        public WarehouseContext()
        {
        }

        public WarehouseContext(DbContextOptions<WarehouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TWarehouseOutbound> TWarehouseOutbounds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:WarehouseConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TWarehouseOutbound>(entity =>
            {
                entity.HasKey(e => e.OutboundNo);

                entity.ToTable("T_WarehouseOutbound");

                entity.Property(e => e.OutboundNo).ValueGeneratedNever();

                entity.Property(e => e.OutboundDate).HasColumnType("date");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SparepartDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
