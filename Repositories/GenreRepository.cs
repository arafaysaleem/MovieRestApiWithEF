using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(MySqlDbContext db) : base(db) { }

        public async Task<IEnumerable<Genre>> GetAllGenres(bool details)
        {
            if (details)
            {
                throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public async Task<Genre?> GetGenreById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Genre?> GetGenreMovies(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GenreExists(string name) => await ExistsAsync(o => o.Name.Equals(name));

        public async Task<bool> GenreExists(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public void CreateGenre(Genre Genre) => Create(Genre);

        public void DeleteGenre(int id) => Delete(new Genre() { Id = id });

        public void UpdateGenre(Genre Genre) => Update(Genre);
    }
}
