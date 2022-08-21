using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MovieAppDbContext db) : base(db) { }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email) => await ExistsAsync(o => o.Email.Equals(email));

        public async Task<bool> UserExists(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public void CreateUser(User User) => Create(User);

        public void DeleteUser(int id) => Delete(new User() { Id = id });

        public void UpdateUser(User User) => Update(User);
    }
}
