using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PersonaService.Models;

namespace PersonaService.Context
{
    public partial class PersonaContext : DbContext
    {
        public PersonaContext()
        {
        }

        public PersonaContext(DbContextOptions<PersonaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MPersona> MPersonas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:PersonaConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MPersona>(entity =>
            {
                entity.HasKey(e => e.PersonaNo);

                entity.ToTable("M_Persona");

                entity.Property(e => e.PersonaNo).ValueGeneratedNever();

                entity.Property(e => e.PersonaName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
