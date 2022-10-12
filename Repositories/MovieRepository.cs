using Microsoft.EntityFrameworkCore;
using MovieRestApiWithEF.Core;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Infrastructure;

namespace MovieRestApiWithEF.Application
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(MovieAppDbContext db) : base(db) { }

        public async Task<IEnumerable<Movie>> FindAllAsync()
        {
            return await FindAll()
                .Include(e => e.Cast)
                .Include(e => e.Genre)
                .Include(e => e.Director)
                .ToListAsync();
        }

        public async Task<Movie?> FindByIdAsync(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .Include(e => e.Cast)
                .Include(e => e.Genre)
                .Include(e => e.Director)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsWithTitleAsync(string title) => await ExistsAsync(o => o.Title.Equals(title));

        public async Task<bool> ExistsWithIdAsync(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public new void Create(Movie movie) => base.Create(movie);

        // Empty object is created to allow deleteing by Id. Rest of the details don't matter
        // as long as the Id is the same, Ef core will delete the matching entity from the db
        public void Delete(int id) => Delete(new Movie() { Id = id });

        public new void Update(Movie movie) => base.Update(movie);
    }
}
