using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MySqlDbContext _db;

        public GenreRepository(MySqlDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _db.Read("ReadAllGenres",
                async (reader) =>
                {
                    var genres = new List<Genre>();

                    while (await reader.ReadAsync())
                    {
                        var genre = new Genre()
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                        };
                        genres.Add(genre);
                    }

                    return genres;
                });
        }

        public async Task<Genre?> GetGenreById(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "Id", id }
            };

            return await _db.Read("ReadGenre", paramDict,
                async (reader) =>
                {
                    Genre? genre = null;
                    while (await reader.ReadAsync())
                    {
                        genre = new Genre()
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                        };
                    }

                    return genre;
                });
        }

        public async Task<Genre?> GetGenreMovies(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GenreExists(string name)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", null },
                { "Name", name }
            };

            bool exists = (bool)(await _db.ReadScalar("GenreExists", paramDict) ?? false);

            return exists;
        }

        public async Task<bool> GenreExists(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id },
                { "Name", null }
            };

            bool exists = (bool)(await _db.ReadScalar("GenreExists", paramDict) ?? false);

            return exists;
        }

        public Task<bool> CreateGenre(Genre genre)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "Name", genre.Name }
            };

            return _db.Set<bool>("InsertGenre", paramDict,
                (modified) => modified > 0);
        }
        public Task<bool> UpdateGenre(Genre genre)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "Id", genre.Id },
                { "Name", genre.Name }
            };

            return _db.Set<bool>("UpdateGenre", paramDict,
                (modified) => modified > 0);
        }

        public Task<bool> DeleteGenre(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object>
            {
                { "Id", id }
            };

            return _db.Set<bool>("DeleteGenre", paramDict,
                (modified) => modified > 0);
        }

    }
}
