using MovieRestApiWithEF.API.Extensions;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.UsersControllerTests
{
    public class UsersMockData
    {
        public static List<User> AllUsers()
        {
            return new List<User>{
                 new User{ Id = 1, Email = "user1@email.com", Role = UserRole.ApiUser, Password = "test.pass123" },
                 new User{ Id = 2, Email = "user2@email.com", Role = UserRole.ApiUser, Password = "test.pass123" },
                 new User{ Id = 3, Email = "user3@email.com", Role = UserRole.ApiUser, Password = "test.pass123" },
            };
        }

        public static List<UserResponse> AllUsersResponse()
        {
            return new List<UserResponse>{
                 new UserResponse { Id = 1, Email = "user1@email.com", Role = UserRole.ApiUser.Name() },
                 new UserResponse { Id = 2, Email = "user2@email.com", Role = UserRole.ApiUser.Name() },
                 new UserResponse { Id = 3, Email = "user3@email.com", Role = UserRole.ApiUser.Name() },
            };
        }

        public static List<User> EmptyUsers() => new List<User>();

        public static User SingleUser()
        {
            return new User { Id = 1, Email = "user1@email.com", Role = UserRole.ApiUser, Password = "test.pass123" };
        }

        public static UserResponse SingleUserResponse()
        {
            return new UserResponse { Id = 1, Email = "user1@email.com", Role = UserRole.ApiUser.Name() };
        }
    }
}
