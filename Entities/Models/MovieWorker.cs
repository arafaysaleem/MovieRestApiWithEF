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
}
