using Microsoft.EntityFrameworkCore;
using MovieRestApiWithEF.EfCore;
using MovieRestApiWithEF.Models;

namespace MovieRestApiWithEF.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAppDbContext _db;

        public MovieRepository(MovieAppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _db.Movies
                .AsNoTracking()
                .Include(e => e.Cast)
                .Include(e => e.Genre)
                .Include(e => e.Director)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await _db.Movies
                .AsNoTracking()
                .Include(e => e.Cast)
                .Include(e => e.Genre)
                .Include(e => e.Director)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<bool> MovieExists(int id)
        {
            return await _db.Movies
                .AsNoTracking()
                .AnyAsync(o => o.Id == id);
        }

        public Task<bool> CreateMovie(Movie movie)
        {
            _db.Add<Movie>(movie);
            return Save();
        }

        public Task<bool> DeleteMovie(Movie movie)
        {
            _db.Remove<Movie>(movie);
            return Save();
        }


        public async Task<bool> Save()
        {
            return (await _db.SaveChangesAsync()) >= 0 ? true : false;
        }

        public Task<bool> UpdateMovie(Movie movie)
        {
            _db.Movies.Update(movie);
            return Save();
        }
    }
}
