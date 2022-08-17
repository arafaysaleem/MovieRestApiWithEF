using MovieRestApiWithEF.Models;

namespace MovieRestApiWithEF.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();

        Task<Movie?> GetMovieById(int id);

        Task<bool> MovieExists(int id);

        Task<bool> CreateMovie(Movie movie);

        Task<bool> UpdateMovie(Movie movie);

        Task<bool> DeleteMovie(Movie movie);

        Task<bool> Save();
    }
}
