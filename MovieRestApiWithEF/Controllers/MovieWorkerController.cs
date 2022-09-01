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
        public async Task<IActionResult> GetAll()
        {
            // Fetch all models from db
            var movieWorkers = await _repositoryManager.MovieWorkerRepository.GetAllMovieWorkers();
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
        public async Task<IActionResult> GetOne(int id)
        {
            // Fetch model from db
            var movieWorker = await _repositoryManager.MovieWorkerRepository.GetMovieWorkerById(id);

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
        public async Task<IActionResult> GetOneWithMovies(int id)
        {
            // Fetch model with nested details from db
            var movieWorker = await _repositoryManager.MovieWorkerRepository.GetMovieWorkerMovies(id);

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
        public async Task<IActionResult> Post([FromBody] MovieWorkerCreateRequest movieWorkerReq)
        {
            // Convert Request DTO to Model
            var movieWorker = _mapper.Map<MovieWorker>(movieWorkerReq);

            // Create Movie Worker
            var success = await _repositoryManager.MovieWorkerRepository.CreateMovieWorker(movieWorker);

            if (!success)
            {
                _logger.LogError($"Movie worker with title \"{movieWorkerReq.FullName}\" failed to be inserted.");
                throw new InternalServerException("Movie worker failed to be created");
            }

            // Convert newly created Model to Response DTO
            var movieWorkerResponse = _mapper.Map<MovieWorkerResponse>(movieWorker);

            // Send model with status 201 Created and location of newly created resource and its id
            return CreatedAtAction(nameof(GetOne), new { id = movieWorkerResponse.Id }, movieWorkerResponse);
        }

        // <summary>
        /// Update a movie worker
        /// </summary>
        /// <return></return>
        [HttpPut("{movieWorkerId:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> Update(int movieWorkerId, [FromBody] MovieWorkerCreateRequest movieWorkerReq)
        {
            // Convert Request DTO to Model
            var movieWorker = _mapper.Map<MovieWorker>(movieWorkerReq);
            movieWorker.Id = movieWorkerId;

            // Update model
            var success = await _repositoryManager.MovieWorkerRepository.UpdateMovieWorker(movieWorker);
            
            if (!success)
            {
                _logger.LogError($"Movie worker with id: {movieWorkerId}, hasn't been found in db.");
                throw new NotFoundException("Movie worker not found");
            }

            // Send 204 response
            return NoContent();
        }

        /// <summary>
        /// Update a movie worker
        /// </summary>
        /// <return></return>
        [HttpDelete("{movieWorkerId:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int movieWorkerId)
        {
            // Delete model
            var success = await _repositoryManager.MovieWorkerRepository.DeleteMovieWorker(movieWorkerId);

            if (!success)
            {
                _logger.LogError($"Movie worker with id: {movieWorkerId}, hasn't been found in db.");
                throw new NotFoundException("Movie worker not found");
            }

            // Send 204 response
            return NoContent();
        }
    }
}
