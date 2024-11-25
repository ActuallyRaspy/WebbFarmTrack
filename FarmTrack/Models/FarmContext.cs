using FarmTrack.Models.Entities;
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
        public DbSet<PlantedCrop> PlantedCrops { get; set; }
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
                .HasForeignKey(b => b.FieldId)
                .HasPrincipalKey(b => b.FieldId);

            modelBuilder.Entity<PlantedCrop>()

                .HasMany(b => b.Alerts)
                .WithOne(b => b.PlantedCrop)
                .HasForeignKey(b => b.PlantedCropId)
                .HasPrincipalKey(b => b.PlantedCropId);

            modelBuilder.Entity<PlantedCrop>()
                 .HasOne(p => p.Field)
                 .WithMany(f => f.PlantedCrops)
                 .HasForeignKey(p => p.FieldId)
                 .HasPrincipalKey(f => f.FieldId);

            modelBuilder.Entity<Alert>()
                .HasOne(b => b.PlantedCrop)
                .WithMany(b => b.Alerts)
                .HasForeignKey(b => b.PlantedCropId)
                .HasPrincipalKey(b => b.PlantedCropId)
                .IsRequired(false);

            modelBuilder.Entity<PlantedCrop>()
                .Property(p => p.PlantedCropId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Alert>()
                .Property(p => p.AlertId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Crop>()
                .Property(p => p.CropId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(p => p.UserID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Field>()
                .Property(p => p.FieldId)
                .ValueGeneratedOnAdd();

        }
    }
}

