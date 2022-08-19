namespace Entities.ResponseDtos
{
    public class MovieWorkerWithDetailsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<MovieDto>? DirectedMovies { get; set; }
        public IEnumerable<MovieDto>? ActedMovies { get; set; }
    }
}
