using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Movie>()
                .HasOne(e => e.Director)
                .WithMany(s => s.DirectedMovies)
                .HasForeignKey(e => e.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Movie>()
                .HasOne(e => e.Genre)
                .WithMany(s => s.Movies)
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Movie>()
                .HasMany(p => p.Cast)
                .WithMany(p => p.ActedMovies)
                .UsingEntity<Dictionary<string, object>>("MovieCast",
                    j => j
                        .HasOne<MovieWorker>()
                        .WithMany()
                        .HasForeignKey("CastId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Movie>()
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.NoAction)
                    );
        }
    }
}
