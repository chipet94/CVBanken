//using API_CVPortalen.Helpers.DataSeeding;

using CVBanken.Data.Helpers.Database;
using CVBanken.Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Data.Models.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData.Seed(builder);
           // DataSeeder.Seed(builder);
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Programme> Programmes { get; set; }
    }
}