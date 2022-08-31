using Entities.Models;

namespace Contracts
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenres();

        Task<Genre?> GetGenreById(int id);

        Task<Genre?> GetGenreMovies(int id);

        Task<bool> GenreExists(string Name);

        Task<bool> GenreExists(int id);

        Task<bool> CreateGenre(Genre Genre);

        Task<bool> UpdateGenre(Genre Genre);

        Task<bool> DeleteGenre(int id);
    }
}
