using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task<User?> GetUserByEmail(string email);

        Task<bool> UserExists(string email);

        Task<bool> UserExists(int id);

        Task<bool> CreateUser(User user);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(int id);
    }
}
