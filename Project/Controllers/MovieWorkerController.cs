using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestDtos;
using Entities.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                // Fetch all models from db
                var movieWorkers = await _repositoryManager.MovieWorkerRepository.GetAllMovieWorkers();
                _logger.LogInfo($"Returned all MovieWorkers from database.");

                // Convert Models to Response DTO
                var movieWorkersResult = _mapper.Map<IEnumerable<MovieWorkerDto>>(movieWorkers);

                // Send response with 200 OK
                return Ok(movieWorkersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get All MovieWorkers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get MovieWorker by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                // Fetch model from db
                var movieWorker = await _repositoryManager.MovieWorkerRepository.GetMovieWorkerById(id);
                
                // Check if model not found
                if (movieWorker is null)
                {
                    _logger.LogError($"Movie worker with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned movie worker with id: {id}");

                    // Convert Model to Response DTO
                    var movieWorkerResult = _mapper.Map<MovieWorkerDto>(movieWorker);

                    // Send response with 200 OK
                    return Ok(movieWorkerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get One MovieWorker action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get All Movies of a MovieWorker by Id
        /// </summary>
        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetOneWithMovies(int id)
        {
            try
            {
                // Fetch model with nested details from db
                var movieWorker = await _repositoryManager.MovieWorkerRepository.GetMovieWorkerMovies(id);
                
                // Check if model not found
                if (movieWorker is null)
                {
                    _logger.LogError($"Movie worker with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned movie worker with id: {id}");

                    // Convert Model to Response DTO
                    var movieWorkerResult = _mapper.Map<MovieWorkerWithDetailsDto>(movieWorker);

                    // Send response with 200 OK
                    return Ok(movieWorkerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get One MovieWorker with Movies action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a new movie worker
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieWorkerCreateDto movieWorker)
        {
            try
            {
                // Check if data from client is missing
                if (movieWorker is null)
                {
                    _logger.LogError("Movie worker object sent from client is null.");
                    return BadRequest("Movie worker object is null");
                }

                // Validate data from client
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie worker object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Convert Request DTO to Model
                var movieWorkerEntity = _mapper.Map<MovieWorker>(movieWorker);
                
                // Create Movie Worker
                _repositoryManager.MovieWorkerRepository.CreateMovieWorker(movieWorkerEntity);
                await _repositoryManager.SaveAsync();

                // Convert newly created Model to Response DTO
                var createdMovieWorker = _mapper.Map<MovieWorkerDto>(movieWorkerEntity);

                // Send model with status 201 Created and location of newly created resource and its id
                return CreatedAtAction(nameof(GetOne), new { id = createdMovieWorker.Id }, createdMovieWorker);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Post MovieWorker action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // <summary>
        /// Update a movie worker
        /// </summary>
        /// <return></return>
        [HttpPut("{movieWorkerId:int}")]
        public async Task<IActionResult> Update(int movieWorkerId, [FromBody] MovieWorkerCreateDto movieWorker)
        {
            try
            {
                // Check if data from client is missing
                if (movieWorker is null)
                {
                    _logger.LogError("Movie worker object sent from client is null.");
                    return BadRequest("Movie worker object is null");
                }

                // Validate data from client
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie worker object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Check if movie exists in db
                var movieWorkerExists = await _repositoryManager.MovieWorkerRepository.MovieWorkerExists(movieWorkerId);
                if (!movieWorkerExists)
                {
                    _logger.LogError($"Movie worker with id: {movieWorkerId}, hasn't been found in db.");
                    return NotFound();
                }

                // Convert Request DTO to Model
                var movieWorkerEntity = _mapper.Map<MovieWorker>(movieWorker);
                
                // Update model
                _repositoryManager.MovieWorkerRepository.UpdateMovieWorker(movieWorkerEntity);
                await _repositoryManager.SaveAsync();

                // Send 204 response
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Update movieWorker action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Update a movie worker
        /// </summary>
        /// <return></return>
        [HttpDelete("{movieWorkerId:int}")]
        public async Task<IActionResult> Delete(int movieWorkerId)
        {
            try
            {
                // Check if model exists in db
                var movieWorkerExists = await _repositoryManager.MovieWorkerRepository.MovieWorkerExists(movieWorkerId);
                if (!movieWorkerExists)
                {
                    _logger.LogError($"Movie worker with id: {movieWorkerId}, hasn't been found in db.");
                    return NotFound();
                }

                // Delete model
                _repositoryManager.MovieWorkerRepository.DeleteMovieWorker(movieWorkerId);
                await _repositoryManager.SaveAsync();

                // Send 204 response
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete movieWorker action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
