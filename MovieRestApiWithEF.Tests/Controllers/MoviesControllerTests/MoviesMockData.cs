using Entities.Models;
using Entities.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class MoviesMockData
    {
        private static DateTime DateTime = DateTime.Now;

        public static List<Movie> GetMovies()
        {
            return new List<Movie>{
                 new Movie{
                     Id = 1,
                     Title = "Movie 1",
                     ReleaseDate = DateTime,
                     DirectorId = 1,
                     GenreId = 1,
                     Director = new MovieWorker
                     {
                        Id = 1,
                        FullName = "Director 1",
                        PictureUrl = "some-url",
                     },
                     Genre = new Genre
                     {
                         Id = 1,
                         Name = "Genre 1",
                     },
                     Cast = new List<MovieWorker>(),
                 },
                 new Movie{
                     Id = 2,
                     Title = "Movie 2",
                     ReleaseDate = DateTime,
                     DirectorId = 2,
                     GenreId = 2,
                     Director = new MovieWorker
                     {
                        Id = 2,
                        FullName = "Director 2",
                        PictureUrl = "some-url",
                     },
                     Genre = new Genre
                     {
                         Id = 2,
                         Name = "Genre 2",
                     },
                     Cast = new List<MovieWorker>(),
                 },
                 new Movie{
                     Id = 3,
                     Title = "Movie 3",
                     ReleaseDate = DateTime,
                     DirectorId = 3,
                     GenreId = 3,
                     Director = new MovieWorker
                     {
                        Id = 3,
                        FullName = "Director 3",
                        PictureUrl = "some-url",
                     },
                     Genre = new Genre
                     {
                         Id = 3,
                         Name = "Genre 3",
                     },
                     Cast = new List<MovieWorker>(),
                 },
            };
        }

        public static List<MovieWithDetailsResponse> GetMoviesResponse()
        {
            return new List<MovieWithDetailsResponse>{
                 new MovieWithDetailsResponse
                 {
                     Id = 1,
                     Title = "Movie 1",
                     ReleaseDate = DateTime,
                     Director = new MovieWorkerResponse
                     {
                        Id = 1,
                        FullName = "Director 1",
                        PictureUrl = "some-url"
                     },
                     Genre = new GenreResponse
                     {
                         Id = 1,
                         Name = "Genre 1",
                     },
                     Cast = new List<MovieWorkerResponse>(),
                 },
                 new MovieWithDetailsResponse
                 {
                     Id = 2,
                     Title = "Movie 2",
                     ReleaseDate = DateTime,
                     Director = new MovieWorkerResponse
                     {
                        Id = 2,
                        FullName = "Director 2",
                        PictureUrl = "some-url"
                     },
                     Genre = new GenreResponse
                     {
                         Id = 2,
                         Name = "Genre 2",
                     },
                     Cast = new List<MovieWorkerResponse>(),
                 },
                 new MovieWithDetailsResponse
                 {
                     Id = 3,
                     Title = "Movie 3",
                     ReleaseDate = DateTime,
                     Director = new MovieWorkerResponse
                     {
                        Id = 3,
                        FullName = "Director 3",
                        PictureUrl = "some-url"
                     },
                     Genre = new GenreResponse
                     {
                         Id = 3,
                         Name = "Genre 3",
                     },
                     Cast = new List<MovieWorkerResponse>(),
                 },
            };
        }

        public static List<Movie> GetEmptyMovies()
        {
            return new List<Movie>();
        }

        public static List<MovieResponse> GetEmptyMoviesResponse()
        {
            return new List<MovieResponse>();
        }
    }
}
