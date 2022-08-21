using Entities.Models;
using System.Security.Claims;

namespace Contracts
{
    public interface IJwtService
    {
        public string BuildToken(User user);

        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
    }
}
