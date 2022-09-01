using Entities.Models;

namespace Contracts
{
    public interface IMovieWorkerRepository
    {
        Task<IEnumerable<MovieWorker>> GetAllMovieWorkers();

        Task<MovieWorker?> GetMovieWorkerById(int id);

        Task<MovieWorker?> GetMovieWorkerMovies(int id);

        Task<bool> MovieWorkerExists(int id);

        Task<bool> CreateMovieWorker(MovieWorker MovieWorker);

        Task<bool> UpdateMovieWorker(MovieWorker MovieWorker);

        Task<bool> DeleteMovieWorker(int id);
    }
}
