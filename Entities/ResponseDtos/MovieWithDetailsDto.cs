namespace Entities.ResponseDtos
{
    public class MovieWithDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GenreDto Genre { get; set; }
        public MovieWorkerDto Director { get; set; }
        public IEnumerable<MovieWorkerDto> Cast { get; set; }
    }
}
