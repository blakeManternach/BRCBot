using BRCBotApi.Models.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace BRCBotApi.Services
{
    public class BRCDbContext : DbContext
    {
        public BRCDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Roll> Rolls { get; set; }
    }
}
