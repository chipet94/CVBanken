//using API_CVPortalen.Helpers.DataSeeding;

using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Site;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Data.Models.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Lia> Lias { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserFile> Files { get; set; }
        public DbSet<UserCv> CvFiles { get; set; }
        public DbSet<SiteMessage> SiteMessages { get; set; }

        public DbSet<ProfilePicture> ProfilePictures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<ProfilePicture>().Property(p => p.Url).HasDefaultValue(ProfilePictureBuilder.NewUrl(15));
            //builder.Entity<User>().HasOne(p => p.Profile).WithOne(u => u.User).HasForeignKey<Profile>(p => p.UserId);
            //builder.Entity<User>().HasOne(p => p.Profile).WithOne(u => u.User).HasForeignKey<Profile>(p => p.UserId);
            base.OnModelCreating(builder);
            //SeedData.Seed(builder);
            // DataSeeder.Seed(builder);
        }
    }
}