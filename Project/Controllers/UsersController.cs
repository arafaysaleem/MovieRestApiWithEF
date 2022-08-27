﻿using AutoMapper;
using Contracts;
using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRestApiWithEF.Exceptions;

namespace UserRestApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public UsersController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all Users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch all users
            var users = await _repositoryManager.UserRepository.GetAllUsers();
            _logger.LogInfo($"Returned all users from database.");

            // Convert Model to Response DTO
            var usersResult = _mapper.Map<IEnumerable<UserResponse>>(users);
            return Ok(usersResult);
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            // Fetch user
            var user = await _repositoryManager.UserRepository.GetUserById(id);

            // Check if user not exists
            if (user is null)
            {
                _logger.LogError($"User with id: {id}, hasn't been found in db.");
                throw new NotFoundException("User not found");
            }
            else
            {
                _logger.LogInfo($"Returned user with id: {id}");

                // Convert Model to Response DTO
                var userResult = _mapper.Map<UserResponse>(user);
                return Ok(userResult);
            }
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <return></return>
        [HttpDelete("{userId:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int userId)
        {
            // Check if user exists
            var userExists = await _repositoryManager.UserRepository.UserExists(userId);
            if (!userExists)
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                throw new NotFoundException("User not found");
            }

            // Delete user
            _repositoryManager.UserRepository.DeleteUser(userId);
            await _repositoryManager.SaveAsync();

            // Return 204 response
            return NoContent();
        }
    }
}
