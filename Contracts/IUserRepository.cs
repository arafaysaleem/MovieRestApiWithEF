using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllAsync();

        Task<User?> FindByIdAsync(int id);

        Task<User?> FindByEmailAsync(string email);

        Task<bool> ExistsWithEmailAsync(string Email);

        Task<bool> ExistsWithIdAsync(int id);

        void Create(User User);

        void Update(User User);

        void Delete(int id);
    }
}
