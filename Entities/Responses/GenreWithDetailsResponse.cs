namespace Entities.Responses
{
    public class GenreWithDetailsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieResponse>? Movies { get; set; }
    }
}
