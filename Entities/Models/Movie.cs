using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Movie
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
}
