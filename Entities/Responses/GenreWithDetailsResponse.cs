namespace MovieRestApiWithEF.Core.Responses
{
    // Acts as a DTO for Get Genre With Nested Details endpoints
    public class GenreWithDetailsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieResponse>? Movies { get; set; }
    }
}
