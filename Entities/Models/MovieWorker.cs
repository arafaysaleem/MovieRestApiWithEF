using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MovieWorker
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PictureUrl { get; set; }

        public ICollection<Movie>? ActedMovies { get; set; }

        public ICollection<Movie>? DirectedMovies { get; set; }
    }

    public class MovieWorkerConfiguration : IEntityTypeConfiguration<MovieWorker>
    {
        public void Configure(EntityTypeBuilder<MovieWorker> builder)
        {
            // Used to pre-seed data or build relationships.
            builder.HasData(
                new MovieWorker { Id = 1, FullName = "Christian Bale", PictureUrl = "www.some-url.com/avatar" },
                new MovieWorker { Id = 2, FullName = "Anne Hathaway", PictureUrl = "www.some-url.com/avatar" },
                new MovieWorker { Id = 3, FullName = "Brad Pitt", PictureUrl = "www.some-url.com/avatar" },
                new MovieWorker { Id = 4, FullName = "Micheal Bay", PictureUrl = "www.some-url.com/avatar" },
                new MovieWorker { Id = 5, FullName = "Christopher Nolan", PictureUrl = "www.some-url.com/avatar" }
            );
        }
    }
}
