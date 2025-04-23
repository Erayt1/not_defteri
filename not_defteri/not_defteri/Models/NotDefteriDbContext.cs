using Microsoft.EntityFrameworkCore;
namespace not_defteri.Models
{
    public class NotDefteriDbContext : DbContext
    {
        public NotDefteriDbContext(DbContextOptions<NotDefteriDbContext> options) : base(options) { }

        public DbSet<Not> Nots { get; set; }
    }
}
