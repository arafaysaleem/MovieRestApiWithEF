using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public class MovieWorkerRepository : RepositoryBase<MovieWorker>, IMovieWorkerRepository
    {
        public MovieWorkerRepository(MovieAppDbContext db) : base(db) { }

        public async Task<IEnumerable<MovieWorker>> FindAllAsync(
            Expression<Func<MovieWorker, bool>>? condition,
            bool details,
            bool tracking)
        {
            var results = condition is null ? FindAll(tracking: tracking) : FindByCondition(condition, tracking: tracking);

            if (details)
            {
                return await results
                        .Include(e => e.ActedMovies)
                        .Include(e => e.DirectedMovies)
                        .ToListAsync();

            }
            return await results.ToListAsync();
        }

        public async Task<MovieWorker?> FindByIdAsync(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<MovieWorker?> FindMovieWorkerMoviesAsync(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .Include(e => e.ActedMovies)
                .Include(e => e.DirectedMovies)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsWithIdAsync(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public new void Create(MovieWorker MovieWorker) => base.Create(MovieWorker);

        // Empty object is created to allow deleteing by Id. Rest of the details don't matter
        // as long as the Id is the same, Ef core will delete the matching entity from the db
        public void Delete(int id) => Delete(new MovieWorker() { Id = id });

        public new void Update(MovieWorker MovieWorker) => base.Update(MovieWorker);
    }
}
