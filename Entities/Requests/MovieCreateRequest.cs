using System.ComponentModel.DataAnnotations;

namespace Entities.Requests
{
    public class MovieCreateRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(30, ErrorMessage = "Title can't be longer than 30 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Genre id is required")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Director id is required")]
        public int DirectorId { get; set; }

        [Required(ErrorMessage = "Cast ids are required")]
        public IEnumerable<int> CastIds { get; set; }
    }
}
