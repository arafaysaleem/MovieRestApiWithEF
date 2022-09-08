using Contracts;
using Entities;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MySqlDbContext _dbContext;

        // Holds a singleton instance of movie repository
        private IMovieRepository? _movie;

        // Holds a singleton instance of genre repository
        private IGenreRepository? _genre;

        // Holds a singleton instance of movie worker repository
        private IMovieWorkerRepository? _movieWorker;

        // Holds a singleton instance of user repository
        private IUserRepository? _user;

        public RepositoryManager(MySqlDbContext db)
        {
            _dbContext = db;
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

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}