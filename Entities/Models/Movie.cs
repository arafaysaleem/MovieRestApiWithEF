using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Movie : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DirectorId { get; set; }
        public MovieWorker Director { get; set; }
        public ICollection<MovieWorker> Cast { get; set; }
    }

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> entityBuilder)
        {
            // Building a 1-M relationship and delete behaviours.
            entityBuilder
                .HasOne(e => e.Director)
                .WithMany(s => s.DirectedMovies)
                .HasForeignKey(e => e.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Building a 1-M relationship and delete behaviours.
            entityBuilder
                .HasOne(e => e.Genre)
                .WithMany(s => s.Movies)
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Building a M-M relationship with custom table name and delete behaviours.
            entityBuilder
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
