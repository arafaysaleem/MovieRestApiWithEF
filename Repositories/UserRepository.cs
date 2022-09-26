using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MovieAppDbContext db) : base(db) { }

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<User?> FindByIdAsync(int id)
        {
            return await FindByCondition(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await FindByCondition(e => e.Email.Equals(email))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsWithEmailAsync(string email) => await ExistsAsync(o => o.Email.Equals(email));

        public async Task<bool> ExistsWithIdAsync(int id) => await ExistsAsync(o => o.Id.Equals(id));

        public new void Create(User User) => base.Create(User);

        // Empty object is created to allow deleteing by Id. Rest of the details don't matter
        // as long as the Id is the same, Ef core will delete the matching entity from the db
        public void Delete(int id) => Delete(new User() { Id = id });

        public new void Update(User User) => base.Update(User);
    }
}
