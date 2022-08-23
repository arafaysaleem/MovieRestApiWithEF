namespace Entities.Responses
{
    // Acts as a DTO for Get Movie endpoints
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
