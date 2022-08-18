namespace Contracts
{
    public interface IRepositoryManager
    {
        public IMovieRepository MovieRepository { get; }
        Task<bool> SaveAsync();

        bool Save();
    }
}
