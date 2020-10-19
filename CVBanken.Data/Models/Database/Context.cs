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
            
            // builder.Entity<ProfilePicture>().Property(p => p.Url).HasDefaultValue(ProfilePictureBuilder.NewUrl(15));
            //builder.Entity<User>().HasOne(p => p.Profile).WithOne(u => u.User).HasForeignKey<Profile>(p => p.UserId);
            //builder.Entity<User>().HasOne(p => p.Profile).WithOne(u => u.User).HasForeignKey<Profile>(p => p.UserId);
            base.OnModelCreating(builder);
            SeedData.Seed(builder);
           // DataSeeder.Seed(builder);
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<UserFile> Files { get; set; }
        
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
    }
}