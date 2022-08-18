using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MovieWorker
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Worker fullname is required")]
        [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Worker picture url is required")]
        public string PictureUrl { get; set; }

        public ICollection<Movie> ActedMovies { get; set; }

        public ICollection<Movie> DirectedMovies { get; set; }
    }
}
