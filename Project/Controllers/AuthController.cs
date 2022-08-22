using Contracts;
using Entities.Requests;
using Entities.ResponseDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRestApiWithEF.Extensions;

namespace MovieRestApiWithEF.Controllers
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
        public async Task<IActionResult> Login([FromBody] LoginRequest loginReq)
        {
            try
            {
                // Check if data from client is missing
                if (loginReq is null)
                {
                    _logger.LogError("Login details sent from client is null.");
                    return BadRequest("Login details is null");
                }

                // Validate data from client
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid login details sent from client.");
                    return BadRequest("Invalid model details");
                }

                // Check if email found
                var user = await _repositoryManager.UserRepository.GetUserByEmail(loginReq.Email!);
                if (user == null)
                {
                    _logger.LogError($"User with email: {loginReq.Email}, hasn't been found in db.");
                    return NotFound();
                }

                // Check if password not match
                if (loginReq.Password != user.Password) 
                {
                    _logger.LogError($"Incorrect password for user with email: {loginReq.Email}.");
                    return Unauthorized();
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
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Login action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
