using System.ComponentModel.DataAnnotations;

namespace MovieRestApiWithEF.Core.Requests
{
    // Acts as a DTO for Create Genre endpoints
    public class GenreCreateRequest
    {
        [Required(ErrorMessage = "Genre name is required")]
        [StringLength(10, ErrorMessage = "Genre name can't be longer than 10 characters")]
        public string Name { get; set; }
    }
}
