using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(MovieAppDbContext db) : base(db) { }

        public async Task<IEnumerable<Genre>> FindAllAsync(bool details)
        {
            if (details)
            {
                return await FindAll()
                    .Include(e => e.Movies)
                    .ToListAsync();
            }
            return await FindAll().ToListAsync();
        }

        public async Task<Genre?> FindByIdAsync(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Genre?> FindGenreMoviesAsync(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .Include(e => e.Movies)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsWithNameAsync(string name) => await ExistsAsync(o => o.Name.Equals(name));

        public async Task<bool> ExistsWithIdAsync(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public new void Create(Genre Genre) => base.Create(Genre);

        // Empty object is created to allow deleteing by Id. Rest of the details don't matter
        // as long as the Id is the same, Ef core will delete the matching entity from the db
        public void Delete(int id) => Delete(new Genre() { Id = id });

        public new void Update(Genre Genre) => base.Update(Genre);
    }
}
