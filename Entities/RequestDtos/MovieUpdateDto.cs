using System.ComponentModel.DataAnnotations;

namespace Entities.RequestDtos
{
    public class MovieUpdateDto
    {
        [StringLength(30, ErrorMessage = "Title can't be longer than 30 characters")]
        public string Title { get; set; }

        public int GenreId { get; set; }

        public int DirectorId { get; set; }

        public IEnumerable<int> CastIds { get; set; }
    }
}
