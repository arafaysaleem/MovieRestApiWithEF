namespace MovieRestApiWithEF.Core.Responses
{
    // Acts as a DTO for Get Movie Worker With Details endpoints
    public class MovieWorkerWithDetailsResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<MovieResponse>? DirectedMovies { get; set; }
        public IEnumerable<MovieResponse>? ActedMovies { get; set; }
    }
}
