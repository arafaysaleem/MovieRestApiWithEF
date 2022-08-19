using Contracts;
using Entities;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MovieAppDbContext _dbContext;
        private IMovieRepository _movie;

        public RepositoryManager(MovieAppDbContext MovieAppDbContext)
        {
            _dbContext = MovieAppDbContext;
        }

        public IMovieRepository MovieRepository
        {
            get
            {
                if (_movie == null)
                {
                    _movie = new MovieRepository(_dbContext);
                }

                return _movie;
            }
        }

        public async Task<bool> SaveAsync()
        {
            return (await _dbContext.SaveChangesAsync()) >= 0;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}