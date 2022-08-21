using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(MovieAppDbContext db) : base(db) { }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await FindAll()
                .Include(e => e.Cast)
                .Include(e => e.Genre)
                .Include(e => e.Director)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .Include(e => e.Cast)
                .Include(e => e.Genre)
                .Include(e => e.Director)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> MovieExists(string title) => await ExistsAsync(o => o.Title.Equals(title));

        public async Task<bool> MovieExists(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public void CreateMovie(Movie movie) => Create(movie);

        public void DeleteMovie(int id) => Delete(new Movie() { Id = id });

        public void UpdateMovie(Movie movie) => Update(movie);
    }
}
