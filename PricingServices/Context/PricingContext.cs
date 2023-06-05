using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PricingServices.Models;

namespace PricingServices.Context
{
    public partial class PricingContext : DbContext
    {
        public PricingContext()
        {
        }

        public PricingContext(DbContextOptions<PricingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MPricing> MPricings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:PricingConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MPricing>(entity =>
            {
                //entity.HasKey(e => new { e.Id, e.ProductNo, e.StoreNo, e.CustomerTypeNo, e.ValidFrom, e.ValidTo, e.Amount });

                entity.ToTable("M_Pricing");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
