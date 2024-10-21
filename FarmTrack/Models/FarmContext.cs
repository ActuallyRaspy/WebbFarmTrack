using Microsoft.EntityFrameworkCore;

namespace FarmTrack.Models
{
    public class FarmContext : DbContext
    {
        public DbSet<User> Users{ get; set; }

        public FarmContext(DbContextOptions options) : base(options)
        {

        }
    }
}
