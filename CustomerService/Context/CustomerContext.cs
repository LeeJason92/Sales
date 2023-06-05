using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CustomerService.Models;

namespace CustomerService.Context
{
    public partial class CustomerContext : DbContext
    {
        public CustomerContext()
        {
        }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MCustomer> MCustomers { get; set; } = null!;
        public virtual DbSet<MCustomerType> MCustomerTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:PersonaConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerNo);

                entity.ToTable("M_Customer");

                entity.Property(e => e.CustomerNo).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MCustomerType>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeNo);

                entity.ToTable("M_CustomerType");

                entity.Property(e => e.CustomerTypeNo).ValueGeneratedNever();

                entity.Property(e => e.CustomerTypeName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
