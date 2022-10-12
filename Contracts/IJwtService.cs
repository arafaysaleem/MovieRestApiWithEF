using MovieRestApiWithEF.Core.Models;
using System.Security.Claims;

namespace MovieRestApiWithEF.Infrastructure
{
    public interface IJwtService
    {
        string BuildToken(User user);

        ClaimsPrincipal ValidateToken(string token);
    }
}
