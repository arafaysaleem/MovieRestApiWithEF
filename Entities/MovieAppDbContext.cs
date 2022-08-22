using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Applies settings for entity table, its relationships, properties and seeder data
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new MovieWorkerConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
        }
    }
}
