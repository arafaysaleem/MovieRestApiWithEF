using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Genre
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Genre name is required")]
        [StringLength(10, ErrorMessage = "Genre name can't be longer than 10 characters")]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
