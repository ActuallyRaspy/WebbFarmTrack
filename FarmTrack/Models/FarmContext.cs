﻿using FarmTrack.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmTrack.Models
{
    public class FarmContext : DbContext
    {

        public FarmContext(DbContextOptions<FarmContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Crop> Crop { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<PlantedCrop> PlantedCrop { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(b => b.Fields)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.FieldId)
                .HasPrincipalKey(b => b.UserID);

            modelBuilder.Entity<Crop>()
                .HasKey(b => b.CropId);

            modelBuilder.Entity<Field>()
                .HasMany(b => b.PlantedCrops)
                .WithOne(b => b.Field)
                .HasForeignKey(b => b.PlantedCropId)
                .HasPrincipalKey(b => b.FieldId);

            modelBuilder.Entity<PlantedCrop>()
                .HasMany(b => b.Alerts)
                .WithOne(b => b.PlantedCrop)
                .HasForeignKey(b => b.AlertId)
                .HasPrincipalKey(b => b.PlantedCropId);

            modelBuilder.Entity<PlantedCrop>()
                .HasOne(b => b.Field)
                .WithMany(b => b.PlantedCrops)
                .HasForeignKey(b => b.PlantedCropId)
                .HasPrincipalKey(b => b.FieldId);

            modelBuilder.Entity<Alert>()
                .HasOne(b => b.PlantedCrop)
                .WithMany(b => b.Alerts)
                .HasForeignKey(b => b.AlertId)
                .HasPrincipalKey(b => b.PlantedCropId);

        }
    }
}

