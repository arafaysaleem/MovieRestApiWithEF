using Entities.Models;

namespace Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> FindAllAsync();

        Task<Movie?> FindByIdAsync(int id);

        Task<bool> ExistsWithTitleAsync(string Title);

        Task<bool> ExistsWithIdAsync(int id);

        void Create(Movie movie);

        void Update(Movie movie);

        void Delete(int id);
    }
}
