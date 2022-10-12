using MovieRestApiWithEF.Core;
using MovieRestApiWithEF.Infrastructure;
using Repositories;

namespace MovieRestApiWithEF.Application
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MovieAppDbContext _dbContext;

        // Holds a singleton instance of movie repository
        private IMovieRepository? _movie;

        // Holds a singleton instance of genre repository
        private IGenreRepository? _genre;

        // Holds a singleton instance of movie worker repository
        private IMovieWorkerRepository? _movieWorker;

        // Holds a singleton instance of user repository
        private IUserRepository? _user;

        public RepositoryManager(MovieAppDbContext MovieAppDbContext)
        {
            _dbContext = MovieAppDbContext;
        }

        public IMovieRepository MovieRepository
        {
            // Lazy access to a repository
            get { return _movie ??= new MovieRepository(_dbContext); }
        }

        public IUserRepository UserRepository
        {
            // Lazy access to a repository
            get { return _user ??= new UserRepository(_dbContext); }
        }

        public IGenreRepository GenreRepository
        {
            // Lazy access to a repository
            get { return _genre ??= new GenreRepository(_dbContext); }
        }

        public IMovieWorkerRepository MovieWorkerRepository
        {
            // Lazy access to a repository
            get { return _movieWorker ??= new MovieWorkerRepository(_dbContext); }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}