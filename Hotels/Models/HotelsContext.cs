using Microsoft.EntityFrameworkCore;

namespace Hotels.Models
{
    public class HotelsContext : DbContext
    {
        public HotelsContext()
        {
        }

        public HotelsContext(DbContextOptions<HotelsContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
    }
}
