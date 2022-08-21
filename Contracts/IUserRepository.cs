using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task<User?> GetUserByEmail(string email);

        Task<bool> UserExists(string Email);

        Task<bool> UserExists(int id);

        void CreateUser(User User);

        void UpdateUser(User User);

        void DeleteUser(int id);
    }
}
