using Entities.Models;
using System.Security.Claims;

namespace Contracts
{
    public interface IJwtService
    {
        string BuildToken(User user);

        ClaimsPrincipal ValidateToken(string token);
    }
}
