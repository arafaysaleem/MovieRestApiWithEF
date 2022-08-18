using Contracts;
using Entities;
using Repositories;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private MovieAppDbContext _dbContext;
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
            return (await _dbContext.SaveChangesAsync()) >= 0 ? true : false;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}