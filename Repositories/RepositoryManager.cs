using Contracts;
using Entities;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MovieAppDbContext _dbContext;
        private IMovieRepository? _movie;
        private IGenreRepository? _genre;
        private IMovieWorkerRepository? _movieWorker;
        private IUserRepository? _user;

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

        public IUserRepository UserRepository
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_dbContext);
                }

                return _user;
            }
        }

        public IGenreRepository GenreRepository
        {
            get
            {
                if (_genre == null)
                {
                    _genre = new GenreRepository(_dbContext);
                }

                return _genre;
            }
        }

        public IMovieWorkerRepository MovieWorkerRepository
        {
            get
            {
                if (_movieWorker == null)
                {
                    _movieWorker = new MovieWorkerRepository(_dbContext);
                }

                return _movieWorker;
            }
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