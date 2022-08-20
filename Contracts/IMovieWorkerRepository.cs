using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IMovieWorkerRepository
    {
        Task<IEnumerable<MovieWorker>> GetAllMovieWorkers(
            Expression<Func<MovieWorker, bool>>? condition = null,
            bool details = false,
            bool tracking = false
        );

        Task<MovieWorker?> GetMovieWorkerById(int id);

        Task<MovieWorker?> GetMovieWorkerMovies(int id);

        Task<bool> MovieWorkerExists(int id);

        void CreateMovieWorker(MovieWorker MovieWorker);

        void UpdateMovieWorker(MovieWorker MovieWorker);

        void DeleteMovieWorker(int id);
    }
}
