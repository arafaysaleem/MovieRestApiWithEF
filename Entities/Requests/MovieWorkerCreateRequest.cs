using System.ComponentModel.DataAnnotations;

namespace MovieRestApiWithEF.Core.Requests
{
    // Acts as a DTO for Create Movie Worker endpoints
    public class MovieWorkerCreateRequest
    {
        [Required(ErrorMessage = "Worker fullname is required")]
        [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Worker picture url is required")]
        public string PictureUrl { get; set; }
    }
}
