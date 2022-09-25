using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRestApiWithEF.Exceptions;
using MovieRestApiWithEF.Filters;

namespace MovieRestApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieWorkersController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public MovieWorkersController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all MovieWorkers
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllMovieWorkersAsync()
        {
            // Fetch all models from db
            var movieWorkers = await _repositoryManager.MovieWorkerRepository.FindAllAsync();
            _logger.LogInfo($"Returned all MovieWorkers from database.");

            // Convert Models to Response DTO
            var movieWorkersResult = _mapper.Map<IEnumerable<MovieWorkerResponse>>(movieWorkers);

            // Send response with 200 OK
            return Ok(movieWorkersResult);
        }

        /// <summary>
        /// Get MovieWorker by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneMovieWorkerAsync(int id)
        {
            // Fetch model from db
            var movieWorker = await _repositoryManager.MovieWorkerRepository.FindByIdAsync(id);

            // Check if model not found
            if (movieWorker is null)
            {
                _logger.LogError($"Movie worker with id: {id}, hasn't been found in db.");
                throw new NotFoundException("Movie worker not found");
            }
            else
            {
                _logger.LogInfo($"Returned movie worker with id: {id}");

                // Convert Model to Response DTO
                var movieWorkerResult = _mapper.Map<MovieWorkerResponse>(movieWorker);

                // Send response with 200 OK
                return Ok(movieWorkerResult);
            }
        }

        /// <summary>
        /// Get All Movies of a MovieWorker by Id
        /// </summary>
        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetOneMovieWorkerWithMoviesAsync(int id)
        {
            // Fetch model with nested details from db
            var movieWorker = await _repositoryManager.MovieWorkerRepository.FindMovieWorkerMoviesAsync(id);

            // Check if model not found
            if (movieWorker is null)
            {
                _logger.LogError($"Movie worker with id: {id}, hasn't been found in db.");
                throw new NotFoundException("Movie worker not found");
            }
            else
            {
                _logger.LogInfo($"Returned movie worker with id: {id}");

                // Convert Model to Response DTO
                var movieWorkerResult = _mapper.Map<MovieWorkerWithDetailsResponse>(movieWorker);

                // Send response with 200 OK
                return Ok(movieWorkerResult);
            }
        }

        /// <summary>
        /// Create a new movie worker
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> PostMovieWorkerAsync([FromBody] MovieWorkerCreateRequest movieWorkerReq)
        {
            // Convert Request DTO to Model
            var movieWorker = _mapper.Map<MovieWorker>(movieWorkerReq);

            // Create Movie Worker
            _repositoryManager.MovieWorkerRepository.Create(movieWorker);
            await _repositoryManager.SaveAsync();

            // Convert newly created Model to Response DTO
            var movieWorkerResponse = _mapper.Map<MovieWorkerResponse>(movieWorker);

            // Send model with status 201 Created and location of newly created resource and its id
            return CreatedAtAction(nameof(GetOneMovieWorkerAsync), new { id = movieWorkerResponse.Id }, movieWorkerResponse);
        }

        // <summary>
        /// Update a movie worker
        /// </summary>
        /// <return></return>
        [HttpPut("{movieWorkerId:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> UpdateMovieWorkerAsync(int movieWorkerId, [FromBody] MovieWorkerCreateRequest movieWorkerReq)
        {
            // Check if movie exists in db
            var movieWorkerExists = await _repositoryManager
                .MovieWorkerRepository
                .ExistsWithIdAsync(movieWorkerId);
            if (!movieWorkerExists)
            {
                _logger.LogError($"Movie worker with id: {movieWorkerId}, hasn't been found in db.");
                throw new NotFoundException("Movie worker not found");
            }

            // Convert Request DTO to Model
            var movieWorker = _mapper.Map<MovieWorker>(movieWorkerReq);
            movieWorker.Id = movieWorkerId;

            // Update model
            _repositoryManager.MovieWorkerRepository.Update(movieWorker);
            await _repositoryManager.SaveAsync();

            // Send 204 response
            return NoContent();
        }

        /// <summary>
        /// Update a movie worker
        /// </summary>
        /// <return></return>
        [HttpDelete("{movieWorkerId:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteMovieWorkerAsync(int movieWorkerId)
        {
            // Check if model exists in db
            var movieWorkerExists = await _repositoryManager.MovieWorkerRepository.ExistsWithIdAsync(movieWorkerId);
            if (!movieWorkerExists)
            {
                _logger.LogError($"Movie worker with id: {movieWorkerId}, hasn't been found in db.");
                throw new NotFoundException("Movie worker not found");
            }

            // Delete model
            _repositoryManager.MovieWorkerRepository.Delete(movieWorkerId);
            await _repositoryManager.SaveAsync();

            // Send 204 response
            return NoContent();
        }
    }
}
