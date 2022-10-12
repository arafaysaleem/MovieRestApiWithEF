using Microsoft.EntityFrameworkCore;
using MovieRestApiWithEF.Core.Models;

namespace MovieRestApiWithEF.Core
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        // Internally references the Genres and MovieWorkers table bcz of relationship
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
