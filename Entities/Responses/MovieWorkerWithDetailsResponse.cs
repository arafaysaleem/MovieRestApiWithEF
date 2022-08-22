namespace Entities.Responses
{
    public class MovieWorkerWithDetailsResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<MovieResponse>? DirectedMovies { get; set; }
        public IEnumerable<MovieResponse>? ActedMovies { get; set; }
    }
}
