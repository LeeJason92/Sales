using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrderServices.Models;

namespace OrderServices.Context
{
    public partial class OrderContext : DbContext
    {
        public OrderContext()
        {
        }

        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCancelOrder> TCancelOrders { get; set; } = null!;
        public virtual DbSet<TDeliveryOrder> TDeliveryOrders { get; set; } = null!;
        public virtual DbSet<TSalesOrder> TSalesOrders { get; set; } = null!;
        public virtual DbSet<VOrderDetail> VOrderDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:OrderConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCancelOrder>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderNo, e.Id });

                entity.ToTable("T_CancelOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessUnitName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CancelDate).HasColumnType("date");

                entity.Property(e => e.CancelReason)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SparepartDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StoreDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TDeliveryOrder>(entity =>
            {
                entity.HasKey(e => new { e.DeliveryOrderNo, e.OutboundNo, e.SalesOrderNo, e.Id });

                entity.ToTable("T_DeliveryOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DeliveryOrderDate).HasColumnType("date");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SparepartDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TSalesOrder>(entity =>
            {
                entity.HasKey(e => e.SalesOrderNo)
                    .HasName("PK_T_SalesOder");

                entity.ToTable("T_SalesOrder");

                entity.Property(e => e.SalesOrderNo).ValueGeneratedNever();

                entity.Property(e => e.BusinessUnitName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SalesOrderDate).HasColumnType("date");

                entity.Property(e => e.SparepartDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StoreDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<VOrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_OrderDetails");

                entity.Property(e => e.BusinessUnitName).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.DeliveryDate).HasColumnType("date");

                entity.Property(e => e.ProductDesc).HasMaxLength(200);

                entity.Property(e => e.SalesOrderDate).HasColumnType("date");

                entity.Property(e => e.StatusOrder)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.StoreDesc).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
