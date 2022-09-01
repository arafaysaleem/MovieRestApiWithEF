using Entities.Models;

namespace Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();

        Task<Movie?> GetMovieById(int id);

        Task<bool> MovieExists(string Name);

        Task<bool> MovieExists(int id);

        Task<bool> CreateMovie(Movie Movie);

        Task<bool> UpdateMovie(Movie Movie);

        Task<bool> DeleteMovie(int id);
    }
}
