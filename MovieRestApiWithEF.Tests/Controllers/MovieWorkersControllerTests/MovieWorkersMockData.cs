using Entities.Models;
using Entities.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MovieWorkersControllerTests
{
    public class MovieWorkersMockData
    {
        private static DateTime TestDateTime = DateTime.Now;

        public static List<MovieWorker> AllMovieWorkers()
        {
            return new List<MovieWorker>{
                 new MovieWorker{ Id = 1, FullName = "MovieWorker 1", PictureUrl = "www.some-url.com/avatar" },
                 new MovieWorker{ Id = 2, FullName = "MovieWorker 2", PictureUrl = "www.some-url.com/avatar" },
                 new MovieWorker{ Id = 3, FullName = "MovieWorker 3", PictureUrl = "www.some-url.com/avatar" },
            };
        }

        public static List<MovieWorkerResponse> AllMovieWorkersResponse()
        {
            return new List<MovieWorkerResponse>{
                 new MovieWorkerResponse { Id = 1, FullName = "MovieWorker 1", PictureUrl = "www.some-url.com/avatar" },
                 new MovieWorkerResponse { Id = 2, FullName = "MovieWorker 2", PictureUrl = "www.some-url.com/avatar" },
                 new MovieWorkerResponse { Id = 3, FullName = "MovieWorker 3", PictureUrl = "www.some-url.com/avatar" },
            };
        }

        public static List<MovieWorker> EmptyMovieWorkers()
        {
            return new List<MovieWorker>();
        }

        public static MovieWorker SingleMovieWorker()
        {
            return new MovieWorker { Id = 1, FullName = "MovieWorker 1", PictureUrl = "www.some-url.com/avatar" };
        }

        public static MovieWorkerResponse SingleMovieWorkerResponse()
        {
            return new MovieWorkerResponse { Id = 1, FullName = "MovieWorker 1", PictureUrl = "www.some-url.com/avatar" };
        }

        public static MovieWorker SingleMovieWorkerWithActedMovies()
        {
            return new MovieWorker
            {
                Id = 3,
                FullName = "MovieWorker 3",
                PictureUrl = "www.some-url.com/avatar",
                ActedMovies = new List<Movie> {
                    new Movie { Id = 1, Title = "Movie 1", ReleaseDate = TestDateTime },
                    new Movie { Id = 2, Title = "Movie 2", ReleaseDate = TestDateTime },
                }
            };
        }

        public static MovieWorkerWithDetailsResponse SingleMovieWorkerWithActedMoviesResponse()
        {
            return new MovieWorkerWithDetailsResponse
            {
                Id = 3,
                FullName = "MovieWorker 3",
                PictureUrl = "www.some-url.com/avatar",
                ActedMovies = new List<MovieResponse> {
                    new MovieResponse { Id = 1, Title = "Movie 1", ReleaseDate = TestDateTime },
                    new MovieResponse { Id = 2, Title = "Movie 2", ReleaseDate = TestDateTime },
                },
                DirectedMovies = new List<MovieResponse>(),
            };
        }

        public static MovieWorker SingleMovieWorkerWithDirectedMovies()
        {
            return new MovieWorker
            {
                Id = 3,
                FullName = "MovieWorker 3",
                PictureUrl = "www.some-url.com/avatar",
                DirectedMovies = new List<Movie> {
                    new Movie { Id = 1, Title = "Movie 1", ReleaseDate = TestDateTime },
                    new Movie { Id = 2, Title = "Movie 2", ReleaseDate = TestDateTime },
                }
            };
        }

        public static MovieWorkerWithDetailsResponse SingleMovieWorkerWithDirectedMoviesResponse()
        {
            return new MovieWorkerWithDetailsResponse
            {
                Id = 3,
                FullName = "MovieWorker 3",
                PictureUrl = "www.some-url.com/avatar",
                ActedMovies = new List<MovieResponse>(),
                DirectedMovies = new List<MovieResponse> {
                    new MovieResponse { Id = 1, Title = "Movie 1", ReleaseDate = TestDateTime },
                    new MovieResponse { Id = 2, Title = "Movie 2", ReleaseDate = TestDateTime },
                }
            };
        }
    }
}
