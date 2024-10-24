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
        public DbSet<PlantedCrop> PlantedCrop { get; set; }
        public DbSet<Alert> Alerts { get; set; }

    }
}

