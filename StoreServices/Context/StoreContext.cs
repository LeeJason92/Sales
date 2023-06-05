using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreServices.Models;

namespace StoreServices.Context
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MMarketingArea> MMarketingAreas { get; set; } = null!;
        public virtual DbSet<MStore> MStores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:StoreConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MMarketingArea>(entity =>
            {
                entity.HasKey(e => e.AreaNo);

                entity.ToTable("M_MarketingArea");

                entity.Property(e => e.AreaNo).ValueGeneratedNever();

                entity.Property(e => e.AreaDesc)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MStore>(entity =>
            {
                entity.HasKey(e => e.StoreNo);

                entity.ToTable("M_Store");

                entity.Property(e => e.StoreNo).ValueGeneratedNever();

                entity.Property(e => e.StoreAddress)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StoreDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StorePhone)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
