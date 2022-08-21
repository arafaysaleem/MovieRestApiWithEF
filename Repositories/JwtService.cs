using Contracts;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repositories
{
    public class JwtService : IJwtService
    {
        private readonly JwtTokenConfig _jwtTokenConfig;

        public JwtService(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
        }

        private Claim[] BuildClaims(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return claims;
        }

        public string BuildToken(User user)
        {
            // Base 64 encode the secret key from appsettings
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Secret));

            // Create a base 64 encoded signature using 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Setup token options
            var token = new JwtSecurityToken(
                    issuer: _jwtTokenConfig.Issuer, // Who generates the token
                    audience: _jwtTokenConfig.Audience, // Who receives the token
                    notBefore: DateTime.Now, // The starting DateTime from which the token is valid
                    claims: BuildClaims(user), // Custom object encoded in the payload, contains Id, userRole etc.
                    expires: DateTime.Now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration), // Expiration after which token is invalid
                    signingCredentials: creds); // Signature used to validate the token

            // Create and return a string representation of token using tokenOptions
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            // Create signed key for matching with token key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Secret));

            // Setup parameters to validate token
            var parameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
            };

            // Decode token
            var tokenValidator = new JwtSecurityTokenHandler();
            var principal = tokenValidator.ValidateToken(token, parameters, out var validatedToken);

            // Check decoded token's validity
            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            // Return the User (ClaimsPrinciple) from the token
            return principal;
        }
    }
}
