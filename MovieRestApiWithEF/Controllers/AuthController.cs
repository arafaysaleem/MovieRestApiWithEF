using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRestApiWithEF.API.Extensions;
using MovieRestApiWithEF.API.Filters;
using MovieRestApiWithEF.Core.Requests;
using MovieRestApiWithEF.Exceptions;
using MovieRestApiWithEF.Infrastructure;

namespace MovieRestApiWithEF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IJwtService _jwtService;
        public AuthController(
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            IJwtService jwtService)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Login to receive an auth token
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginReq)
        {
            // Check if email found
            var user = await _repositoryManager.UserRepository.FindByEmailAsync(loginReq.Email!);
            if (user == null)
            {
                _logger.LogError($"User with email: {loginReq.Email}, hasn't been found in db.");
                throw new NotFoundException("Email not found");
            }

            // Check if password not match
            if (loginReq.Password != user.Password)
            {
                _logger.LogError($"Incorrect password for user with email: {loginReq.Email}.");
                throw new UnauthorizedException("Incorrect password!");
            }

            // Generate access token
            var token = _jwtService.BuildToken(user);

            // Create Response DTO
            var authResponse = new AuthenticatedResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.Name(),
                Token = token
            };

            // Send response with 200 OK
            return Ok(authResponse);

        }
    }
}
