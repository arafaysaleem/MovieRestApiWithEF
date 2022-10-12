namespace MovieRestApiWithEF.Core.Responses
{
    // Acts as a DTO for successful login endpoints
    public class AuthenticatedResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }
    }
}
