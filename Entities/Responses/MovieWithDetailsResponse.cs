namespace Entities.Responses
{
    // Acts as a DTO for Get Movie with nested info endpoints
    public class MovieWithDetailsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GenreResponse Genre { get; set; }
        public MovieWorkerResponse Director { get; set; }
        public IEnumerable<MovieWorkerResponse> Cast { get; set; }
    }
}
