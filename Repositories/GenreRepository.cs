using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(MovieAppDbContext db) : base(db) { }

        public async Task<IEnumerable<Genre>> GetAllGenres(bool details)
        {
            if (details)
            {
                return await FindAll()
                    .Include(e => e.Movies)
                    .ToListAsync();
            }
            return await FindAll().ToListAsync();
        }

        public async Task<Genre?> GetGenreById(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Genre?> GetGenreMovies(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .Include(e => e.Movies)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> GenreExists(string name) => await ExistsAsync(o => o.Name.Equals(name));

        public async Task<bool> GenreExists(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public void CreateGenre(Genre Genre) => Create(Genre);

        public void DeleteGenre(int id) => Delete(new Genre() { Id = id });

        public void UpdateGenre(Genre Genre) => Update(Genre);
    }
}
