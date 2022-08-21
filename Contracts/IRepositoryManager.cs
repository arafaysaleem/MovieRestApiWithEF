namespace Contracts
{
    public interface IRepositoryManager
    {
        public IMovieRepository MovieRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IMovieWorkerRepository MovieWorkerRepository { get; }
        public IUserRepository UserRepository { get; }
        Task SaveAsync();
        void Save();
    }
}
