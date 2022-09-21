using Entities.Models;

namespace Contracts
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> FindAllAsync(bool details = false);

        Task<Genre?> FindByIdAsync(int id);

        Task<Genre?> FindGenreMoviesAsync(int id);

        Task<bool> ExistsWithNameAsync(string Name);

        Task<bool> ExistsWithIdAsync(int id);

        void Create(Genre Genre);

        void Update(Genre Genre);

        void Delete(int id);
    }
}
