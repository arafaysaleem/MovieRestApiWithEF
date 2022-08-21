using Entities.Models;

namespace Contracts
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenres(bool details = false);

        Task<Genre?> GetGenreById(int id);

        Task<Genre?> GetGenreMovies(int id);

        Task<bool> GenreExists(string Name);

        Task<bool> GenreExists(int id);

        void CreateGenre(Genre Genre);

        void UpdateGenre(Genre Genre);

        void DeleteGenre(int id);
    }
}
