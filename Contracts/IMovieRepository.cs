using Entities.Models;

namespace Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();

        Task<Movie?> GetMovieById(int id);

        Task<bool> MovieExists(String Title);

        Task<bool> MovieExists(int id);

        void CreateMovie(Movie movie);

        void UpdateMovie(Movie movie);

        void DeleteMovie(Movie movie);
    }
}
