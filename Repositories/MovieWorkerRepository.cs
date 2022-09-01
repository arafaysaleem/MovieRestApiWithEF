using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class MovieWorkerRepository : RepositoryBase<MovieWorker>, IMovieWorkerRepository
    {
        public MovieWorkerRepository(MySqlDbContext db) : base(db) { }

        public async Task<IEnumerable<MovieWorker>> GetAllMovieWorkers()
        {
            return await ReadAsync("ReadAllMovieWorkers",
                async (reader) =>
                {
                    var movieWorkers = new List<MovieWorker>();

                    while (await reader.ReadAsync())
                    {
                        var movieWorker = new MovieWorker()
                        {
                            Id = reader.GetInt32("Id"),
                            FullName = reader.GetString("FullName"),
                            PictureUrl = reader.GetString("PictureUrl")
                        };
                        movieWorkers.Add(movieWorker);
                    }

                    return movieWorkers;
                });
        }

        public async Task<MovieWorker?> GetMovieWorkerById(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "Id", id }
            };

            return await ReadAsync("ReadMovieWorker", paramDict,
                async (reader) =>
                {
                    MovieWorker? movieWorker = null;
                    while (await reader.ReadAsync())
                    {
                        movieWorker = new MovieWorker()
                        {
                            Id = reader.GetInt32("Id"),
                            FullName = reader.GetString("FullName"),
                            PictureUrl = reader.GetString("PictureUrl"),
                        };
                    }

                    return movieWorker;
                });
        }

        public Task<MovieWorker?> GetMovieWorkerMovies(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MovieWorkerExists(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id }
            };

            bool exists = (bool)(await ReadScalarAsync("MovieWorkerExists", paramDict) ?? false);

            return exists;
        }

        public Task<bool> CreateMovieWorker(MovieWorker movieWorker)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "FullName", movieWorker.FullName },
                { "PictureUrl", movieWorker.PictureUrl },
            };

            return InsertAsync<bool>("InsertMovieWorker", paramDict,
                (newId) =>
                {
                    movieWorker.Id = newId;
                    return true;
                });
        }

        public Task<bool> UpdateMovieWorker(MovieWorker movieWorker)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "Id", movieWorker.Id },
                { "FullName", movieWorker.FullName },
                { "PictureUrl", movieWorker.PictureUrl }
            };

            return SetAsync<bool>("UpdateMovieWorker", paramDict,
                (modified) => modified > 0);
        }

        public Task<bool> DeleteMovieWorker(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "Id", id }
            };

            return SetAsync<bool>("DeleteMovieWorker", paramDict,
                (modified) => modified > 0);
        }

    }
}
