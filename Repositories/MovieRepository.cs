using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(MySqlDbContext db) : base(db) { }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await ReadAsync("ReadAllMovies",
                async (reader) =>
                {
                    var movies = new List<Movie>();

                    while (await reader.ReadAsync())
                    {
                        var movie = new Movie()
                        {
                            Id = reader.GetInt32("Id"),
                            Title = reader.GetString("Title"),
                            ReleaseDate = reader.GetDateTime("ReleaseDate"),
                        };
                        movies.Add(movie);
                    }

                    return movies;
                });
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id }
            };

            return await ReadAsync("ReadMovie", paramDict,
                async (reader) =>
                {
                    Movie? movie = null;
                    while (await reader.ReadAsync())
                    {
                        movie = new Movie()
                        {
                            Id = reader.GetInt32("Id"),
                            Title = reader.GetString("Title"),
                            ReleaseDate = reader.GetDateTime("ReleaseDate")
                        };
                    }

                    return movie;
                });
        }

        public async Task<bool> MovieExists(string title)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", null },
                { "Title", title }
            };

            bool exists = Convert.ToBoolean(await ReadScalarAsync("MovieExists", paramDict));

            return exists;
        }

        public async Task<bool> MovieExists(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id },
                { "Title", null }
            };

            bool exists = Convert.ToBoolean(await ReadScalarAsync("MovieExists", paramDict));

            return exists;
        }

        public Task<bool> CreateMovie(Movie movie)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Title", movie.Title },
                { "ReleaseDate", movie.ReleaseDate },
                { "GenreId", movie.GenreId },
                { "DirectorId", movie.DirectorId }
            };

            return InsertAsync("InsertMovie", paramDict,
                (newId) =>
                {
                    movie.Id = newId;
                    return true;
                });
        }

        public Task<bool> UpdateMovie(Movie movie)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", movie.Id },
                { "Title", movie.Title },
                { "ReleaseDate", movie.ReleaseDate },
                { "GenreId", movie.GenreId },
                { "DirectorId", movie.DirectorId }
            };

            return InsertAsync("UpdateMovie", paramDict,
                (modified) => modified > 0);
        }

        public Task<bool> DeleteMovie(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id }
            };

            return InsertAsync("DeleteMovie", paramDict,
                (modified) => modified > 0);
        }

    }
}
