using Microsoft.EntityFrameworkCore;

namespace restful_API.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<UserWeatherForecast> UserWeatherForecasts { get; set; }
        public DbSet<JWT> JWTs { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base (options)
        {            
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Login);

            modelBuilder.Entity<UserWeatherForecast>()
                .HasKey(uwf => new { uwf.UserLogin, uwf.WeatherForecastId });

            modelBuilder.Entity<UserWeatherForecast>()
                .HasOne(uwf => uwf.User)
                .WithMany(u => u.UserWeatherForecasts);

            modelBuilder.Entity<UserWeatherForecast>()
                .HasOne(uwf => uwf.WeatherForecast)
                .WithMany(w => w.UserWeatherForecasts);

            modelBuilder.Entity<User>()
                .HasOne(u => u.JWT)
                .WithOne(t => t.User)
                .HasForeignKey<JWT>(t => t.UserLogin);
        }
    }
}
