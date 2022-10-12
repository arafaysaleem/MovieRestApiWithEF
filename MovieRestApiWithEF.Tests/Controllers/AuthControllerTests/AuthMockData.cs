using MovieRestApiWithEF.API.Extensions;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Requests;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.AuthControllerTests
{
    public class AuthMockData
    {
        public static string TestJwtToken = "test.token.123abc";

        public static LoginRequest CorrectLoginDetails()
        {
            return new LoginRequest
            { 
                Email = "test.user@email.com",
                Password = "test.pass123",
            };
        }

        public static LoginRequest IncorrectPasswordLogin()
        {
            return new LoginRequest
            {
                Email = "test.user@email.com",
                Password = "wrong_pass",
            };
        }

        public static LoginRequest UnknownEmailLogin()
        {
            return new LoginRequest
            {
                Email = "wrong_email@email.com",
                Password = "test.pass123",
            };
        }

        public static User ExistingUser()
        {
            return new User { Id = 1, Email = "test.user@email.com", Role = UserRole.ApiUser, Password = "test.pass123" };
        }

        public static AuthenticatedResponse LoginSuccessResponse()
        {
            return new AuthenticatedResponse
            {
                Id = 1,
                Email = "test.user@email.com",
                Role = UserRole.ApiUser.Name(),
                Token = TestJwtToken,
            };
        }
    }
}
