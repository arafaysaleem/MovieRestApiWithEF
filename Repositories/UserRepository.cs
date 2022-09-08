using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MySqlDbContext db) : base(db) { }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await ReadAsync("ReadAllUsers",
                async (reader) =>
                {
                    var users = new List<User>();

                    while (await reader.ReadAsync())
                    {
                        var user = new User()
                        {
                            Id = reader.GetInt32("Id"),
                            Email = reader.GetString("Email"),
                            Password = reader.GetString("Password"),
                            Role = (UserRole)reader.GetInt32("Role"),
                        };
                        users.Add(user);
                    }

                    return users;
                });
        }

        public async Task<User?> GetUserById(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id },
                { "Email", null }
            };

            return await ReadAsync("ReadUser", paramDict,
                async (reader) =>
                {
                    User? user = null;
                    while (await reader.ReadAsync())
                    {
                        user = new User()
                        {
                            Id = reader.GetInt32("Id"),
                            Email = reader.GetString("Email"),
                            Password = reader.GetString("Password"),
                            Role = (UserRole)reader.GetInt32("Role"),
                        };
                    }

                    return user;
                });
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", null },
                { "Email", email }
            };

            return await ReadAsync("ReadUser", paramDict,
                async (reader) =>
                {
                    User? user = null;
                    while (await reader.ReadAsync())
                    {
                        user = new User()
                        {
                            Id = reader.GetInt32("Id"),
                            Email = reader.GetString("Email"),
                            Password = reader.GetString("Password"),
                            Role = (UserRole)reader.GetInt32("Role"),
                        };
                    }

                    return user;
                });
        }

        public async Task<bool> UserExists(string email)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", null },
                { "Email", email }
            };

            bool exists = Convert.ToBoolean(await ReadScalarAsync("UserExists", paramDict));

            return exists;
        }

        public async Task<bool> UserExists(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id },
                { "Email", null }
            };

            bool exists = Convert.ToBoolean(await ReadScalarAsync("UserExists", paramDict));

            return exists;
        }

        public Task<bool> CreateUser(User user)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Email", user.Email },
                { "Password", user.Password },
                { "Role", user.Role }
            };

            return InsertAsync("InsertUser", paramDict,
                (newId) =>
                {
                    user.Id = newId;
                    return true;
                });
        }

        public Task<bool> UpdateUser(User user)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", user.Id },
                { "Email", user.Email },
                { "Password", user.Password },
                { "Role", user.Role }
            };

            return SetAsync("UpdateUser", paramDict,
                (modified) => modified > 0);
        }

        public Task<bool> DeleteUser(int id)
        {
            // Create params
            var paramDict = new Dictionary<string, object?>
            {
                { "Id", id }
            };

            return SetAsync<bool>("DeleteUser", paramDict,
                (modified) => modified > 0);
        }

    }
}
