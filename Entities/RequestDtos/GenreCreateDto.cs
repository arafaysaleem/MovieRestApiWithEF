using System.ComponentModel.DataAnnotations;

namespace Entities.RequestDtos
{
    public class GenreCreateDto
    {
        [Required(ErrorMessage = "Genre name is required")]
        [StringLength(10, ErrorMessage = "Genre name can't be longer than 10 characters")]
        public string Name { get; set; }
    }
}
