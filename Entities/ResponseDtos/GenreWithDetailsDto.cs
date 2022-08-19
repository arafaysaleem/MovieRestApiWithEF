namespace Entities.ResponseDtos
{
    public class GenreWithDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieDto>? Movies { get; set; }
    }
}
