using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TabulatorDynamicQueryBuilder.Entities;

namespace TabulatorDynamicQueryBuilder.Infrastructure
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BasicDatum> BasicData { get; set; } = null!;
        public virtual DbSet<Coordinate> Coordinates { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<Registered> Registereds { get; set; } = null!;
        public virtual DbSet<Timezone> Timezones { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasicDatum>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.HasIndex(e => e.LocationId, "IX_BasicData_LocationID")
                    .IsUnique();

                entity.HasIndex(e => e.LoginId, "IX_BasicData_LoginID")
                    .IsUnique();

                entity.HasIndex(e => e.PictureId, "IX_BasicData_PictureID")
                    .IsUnique();

                entity.HasIndex(e => e.RegisteredId, "IX_BasicData_RegisteredID")
                    .IsUnique();

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Gender).HasColumnType("smallint");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.PictureId).HasColumnName("PictureID");

                entity.Property(e => e.RegisteredId).HasColumnName("RegisteredID");

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<BasicDatum>(d => d.LocationId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Login)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<BasicDatum>(d => d.LoginId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Picture)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<BasicDatum>(d => d.PictureId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Registered)
                    .WithOne(p => p.BasicDatum)
                    .HasForeignKey<BasicDatum>(d => d.RegisteredId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Coordinate>(entity =>
            {
                entity.HasKey(e => e.CoordinatesId);

                entity.Property(e => e.CoordinatesId).HasColumnName("CoordinatesID");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.HasIndex(e => e.CoordinatesId, "IX_Location_CoordinatesID")
                    .IsUnique();

                entity.HasIndex(e => e.TimezoneId, "IX_Location_TimezoneID")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.CoordinatesId).HasColumnName("CoordinatesID");

                entity.Property(e => e.TimezoneId).HasColumnName("TimezoneID");

                entity.HasOne(d => d.Coordinates)
                    .WithOne(p => p.Location)
                    .HasForeignKey<Location>(d => d.CoordinatesId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Timezone)
                    .WithOne(p => p.Location)
                    .HasForeignKey<Location>(d => d.TimezoneId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("Picture");

                entity.Property(e => e.PictureId).HasColumnName("PictureID");
            });

            modelBuilder.Entity<Registered>(entity =>
            {
                entity.ToTable("Registered");

                entity.Property(e => e.RegisteredId).HasColumnName("RegisteredID");

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<Timezone>(entity =>
            {
                entity.ToTable("Timezone");

                entity.Property(e => e.TimezoneId).HasColumnName("TimezoneID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
