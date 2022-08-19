using System.ComponentModel.DataAnnotations;

namespace Entities.RequestDtos
{
    public class MovieWorkerCreateDto
    {
        [Required(ErrorMessage = "Worker fullname is required")]
        [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Worker picture url is required")]
        public string PictureUrl { get; set; }
    }
}
