namespace Entities.Responses
{
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
