using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<bool> ExistsWithEmailAsync(string Email);

        Task<bool> ExistsWithIdAsync(int id);

        void Create(User User);

        void Update(User User);

        void Delete(int id);
    }
}
