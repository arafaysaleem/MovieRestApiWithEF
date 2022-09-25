using Entities.Models;
using Entities.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class GenresMockData
    {
        private static DateTime TestDateTime = DateTime.Now;

        public static List<Genre> GetGenres()
        {
            return new List<Genre>{
                 new Genre{
                     Id = 1,
                     Name = "Genre 1",
                 },
                 new Genre{
                     Id = 2,
                     Name = "Genre 2",
                 },
                 new Genre{
                     Id = 3,
                     Name = "Genre 3",
                 },
            };
        }

        public static List<GenreResponse> GetGenresResponse()
        {
            return new List<GenreResponse>{
                 new GenreResponse
                 {
                     Id = 1,
                     Name = "Genre 1",
                 },
                 new GenreResponse
                 {
                     Id = 2,
                     Name = "Genre 2",
                 },
                 new GenreResponse
                 {
                     Id = 3,
                     Name = "Genre 3",
                 },
            };
        }

        public static List<Genre> GetEmptyGenres()
        {
            return new List<Genre>();
        }

        public static Genre GetGenre()
        {
            return new Genre
            {
                Id = 1,
                Name = "Genre 1",
            };
        }

        public static GenreResponse GetGenreResponse()
        {
            return new GenreResponse
            {
                Id = 1,
                Name = "Genre 1",
            };
        }

        public static Genre GetGenreWithMovies()
        {
            return new Genre
            {
                Id = 3,
                Name = "Genre 3",
                Movies = new List<Movie>
                     {
                         new Movie
                         {
                             Id = 1,
                             Title = "Movie 1",
                             ReleaseDate = TestDateTime,
                         },
                         new Movie
                         {
                             Id = 2,
                             Title = "Movie 2",
                             ReleaseDate = TestDateTime,
                         },
                     }
            };
        }

        public static GenreWithDetailsResponse GetGenreWithMoviesResponse()
        {
            return new GenreWithDetailsResponse
            {
                Id = 3,
                Name = "Genre 3",
                Movies = new List<MovieResponse>
                     {
                         new MovieResponse
                         {
                             Id = 1,
                             Title = "Movie 1",
                             ReleaseDate = TestDateTime,
                         },
                         new MovieResponse
                         {
                             Id = 2,
                             Title = "Movie 2",
                             ReleaseDate = TestDateTime,
                         },
                     }
            };
        }

        public static GenreWithDetailsResponse GetGenreWithEmptyMoviesResponse()
        {
            return new GenreWithDetailsResponse
            {
                Id = 1,
                Name = "Genre 1",
                Movies = new List<MovieResponse>(),
            };
        }
    }
}
