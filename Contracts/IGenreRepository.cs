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

        void CreateGenre(Genre Genre);

        void UpdateGenre(Genre Genre);

        void DeleteGenre(int id);
    }
}
