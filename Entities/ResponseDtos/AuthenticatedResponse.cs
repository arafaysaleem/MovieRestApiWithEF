using Entities.Models;

namespace Entities.ResponseDtos
{
    public class AuthenticatedResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }

        public UserRole Role { get; set; }
    }
}
